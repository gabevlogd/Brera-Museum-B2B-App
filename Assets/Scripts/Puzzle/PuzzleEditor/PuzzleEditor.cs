#if UNITY_EDITOR
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;

public class PuzzleEditor : EditorWindow
{
    private Grid<Toggle> m_GridCoordinates;
    private int m_PuzzleWidth  = 0;
    private int m_PuzzleHeigth = 0;
    private string m_PictureID;
    private string m_PuzzleID;

    private string LEVELS_FOLDER_PATH = "Assets/Scripts/ScriptableObjects/Puzzles";
    private Vector2 m_ScrollPos;

    [MenuItem("Tools/Puzzle Editor")]
    public static void ShowWindow() => GetWindow<PuzzleEditor>("Level Editor");

    private void OnEnable()
    {
        m_GridCoordinates = new Grid<Toggle>(m_PuzzleWidth, m_PuzzleHeigth, 1, new Vector3(-3f, 0f, -3f), (int x, int y) => new Toggle(x, y));
    }
    void OnGUI()
    {
        m_ScrollPos = EditorGUILayout.BeginScrollView(m_ScrollPos);
        DrawPuzzleInformationsInputField();
        DrawPuzzleWidthInputField();
        DrawPuzzleHeigthInputField();
        DrawSaveLevelButton();
        DrawTogglesGrid();
        EditorGUILayout.EndScrollView();
    }

    private void DrawPuzzleInformationsInputField()
    {
        GUILayout.Space(20f);
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("Picture ID: ");
        m_PictureID = EditorGUILayout.TextArea(m_PictureID);
        GUILayout.Label("Puzzle ID: ");
        m_PuzzleID = EditorGUILayout.TextArea(m_PuzzleID);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }

    private void DrawPuzzleWidthInputField()
    {
        GUILayout.Space(20f);
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("Puzzle Width: ");
        m_PuzzleWidth = EditorGUILayout.IntField(m_PuzzleWidth);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }

    private void DrawPuzzleHeigthInputField()
    {
        GUILayout.Space(20f);
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("Puzzle Heigth: ");
        m_PuzzleHeigth = EditorGUILayout.IntField(m_PuzzleHeigth);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }

    private void DrawSaveLevelButton()
    {
        GUILayout.Space(10f);
        if (GUILayout.Button("Save puzzle"))
            PerformSaveLevelButton();
        GUILayout.Space(20f);
    }

    private void PerformSaveLevelButton()
    {
        PuzzleData newLevel = CreateNewPuzzle();
        if (newLevel == null) return;

        if (!Directory.Exists($"{LEVELS_FOLDER_PATH}/{m_PictureID}"))
            AssetDatabase.CreateFolder($"{LEVELS_FOLDER_PATH}", $"{m_PictureID}");

        AssetDatabase.CreateAsset(newLevel, $"{LEVELS_FOLDER_PATH}/{m_PictureID}/Puzzle {m_PuzzleID}.asset");
        AssetDatabase.SaveAssets();
        Debug.Log("New puzzle saved");
    }

    /// <summary>
    /// Draws a grid of toggle for the visual selection of the coordinates
    /// </summary>
    private void DrawTogglesGrid()
    {
        GUILayout.Space(20f);
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("Coordinates on the game grid:");
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.BeginVertical();
        for (int y = 0; y < m_PuzzleHeigth; y++)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            for (int x = 0; x < m_PuzzleWidth; x++)
                m_GridCoordinates.GetGridObject(x, y).Value = GUILayout.Toggle(m_GridCoordinates.GetGridObject(x, y).Value, "", GUILayout.Width(20f), GUILayout.Height(20f));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
    }

    private PuzzleData CreateNewPuzzle()
    {
        PuzzleData newPuzzle = CreateInstance<PuzzleData>();

        newPuzzle.Grid = new Grid<Node>(m_PuzzleWidth, m_PuzzleHeigth, 1, new Vector3(-3f, 0f, -3f), (int x, int y) => new Node(x, y));
        newPuzzle.GridWidth = m_PuzzleWidth;
        newPuzzle.GridHeight = m_PuzzleHeigth;

        for (int y = 0; y < m_PuzzleHeigth; y++)
        {
            for (int x = 0; x < m_PuzzleWidth; x++)
                newPuzzle.Grid.GetGridObject(x, y).SetNode(NodeType.Start, !m_GridCoordinates.GetGridObject(x, y).Value);
        }

        DirectoryInfo info = new DirectoryInfo($"{LEVELS_FOLDER_PATH}/x/y");
        return newPuzzle;
    }
}

#endif
