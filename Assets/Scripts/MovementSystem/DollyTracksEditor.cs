using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Cinemachine;

public class DollyTracksEditor : EditorWindow
{

    private Vector2 _scrollPos;
    private bool _newBranch;



    [MenuItem("Tools/Dolly Tracks Editor")]
    public static void ShowWindow() => GetWindow<DollyTracksEditor>("Dolly Tracks Editor");


    private void OnGUI()
    {
        _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
        DrawNewTrackButton();

        EditorGUILayout.EndScrollView();
    }

    private void DrawNewTrackButton()
    {
        GUILayout.Space(20f);
        if (GUILayout.Button("New Track"))
            PerformNewTrackButton();
    }

    private void PerformNewTrackButton()
    {
        DollyTracksManager dollyTracksManager = FindObjectOfType<DollyTracksManager>();
        CinemachineSmoothPath newTrack = Resources.Load<CinemachineSmoothPath>("Dolly Track");
        if (dollyTracksManager == null)
        {
            dollyTracksManager = MonoBehaviour.Instantiate<DollyTracksManager>(Resources.Load<DollyTracksManager>("DollyTracksManager"));
            dollyTracksManager.TracksList.Add(MonoBehaviour.Instantiate<CinemachineSmoothPath>(newTrack, Vector3.zero, Quaternion.identity, dollyTracksManager.transform));
            dollyTracksManager.TracksList[^1].GetComponent<DollyTrack>().ID = dollyTracksManager.TracksList.Count;
        }
        else
        {
            dollyTracksManager.TracksList.Add(MonoBehaviour.Instantiate<CinemachineSmoothPath>(newTrack, Vector3.zero, Quaternion.identity, dollyTracksManager.transform));
            dollyTracksManager.TracksList[^1].m_Waypoints[0].position = dollyTracksManager.TracksList[dollyTracksManager.TracksList.Count - 2].m_Waypoints[^1].position;
            dollyTracksManager.TracksList[^1].m_Waypoints[1].position = dollyTracksManager.TracksList[^1].m_Waypoints[0].position + new Vector3(0f, 0f, 10f);
            dollyTracksManager.TracksList[^1].GetComponent<DollyTrack>().ID = dollyTracksManager.TracksList.Count;
        }
        //DollyTracksData dollyTracksData = Resources.Load<DollyTracksData>("DollyTracksData");
        //CinemachineSmoothPath trackToSpawn = Resources.Load<CinemachineSmoothPath>("Dolly Track");
        //PrefabUtility.InstantiatePrefab(Resources.Load<CinemachineSmoothPath>("Dolly Track"));

        //if(dollyTracksData.TracksList != null && dollyTracksData.TracksList.Count > 0)
        //    trackToSpawn.m_Waypoints[0].position = dollyTracksData.TracksList[^1].m_Waypoints[^1].position;
        //else
        //{
        //    dollyTracksData.TracksList = new List<CinemachineSmoothPath>();
        //    //dollyTracksData.TracksList.Add()
        //}
        
        
        
        
        

    }
}
