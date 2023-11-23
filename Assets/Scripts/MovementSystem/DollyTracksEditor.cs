using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Cinemachine;

public class DollyTracksEditor : EditorWindow
{
    private DollyTracksManager _dollyTracksManager;
    private Vector2 _scrollPos;
    private CinemachineSmoothPath _anchoredTrack;



    [MenuItem("Tools/Dolly Tracks Editor")]
    public static void ShowWindow() => GetWindow<DollyTracksEditor>("Dolly Tracks Editor");


    private void Awake()
    {
        _dollyTracksManager = FindObjectOfType<DollyTracksManager>();
    }



    private void OnGUI()
    {
        _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
        DrawNewTrackButton();


        if (_dollyTracksManager != null)
        {
            foreach(DollyTrack track in _dollyTracksManager.TracksList)
                EditorGUILayout.ObjectField(track, typeof(DollyTrack), true);
        }

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
        if (_dollyTracksManager == null)
        {
            _dollyTracksManager = FindObjectOfType<DollyTracksManager>();
        }
        else if (Selection.activeGameObject == null)
        {
            Debug.Log("Double click the track you want to link the new one");
            return;
        }

        CinemachineSmoothPath trackPrefabRef = Resources.Load<CinemachineSmoothPath>("Dolly Track");

        if (_dollyTracksManager == null)
        {
            //instantiates the tracks manager
            _dollyTracksManager = MonoBehaviour.Instantiate<DollyTracksManager>(Resources.Load<DollyTracksManager>("DollyTracksManager"));
            //instantiates the new track
            trackPrefabRef = MonoBehaviour.Instantiate<CinemachineSmoothPath>(trackPrefabRef, Vector3.zero, Quaternion.identity, _dollyTracksManager.transform);
            DollyTrack newDollyTrack = trackPrefabRef.GetComponent<DollyTrack>();
            //add new track to manager tracks list
            _dollyTracksManager.TracksList.Add(newDollyTrack);
            //initializes the new track datas
            newDollyTrack.ID = _dollyTracksManager.TracksList.Count;
            newDollyTrack.This = trackPrefabRef;
        }
        else if (Selection.activeGameObject.TryGetComponent(out _anchoredTrack))
        {
            //instantiates the new track
            trackPrefabRef = MonoBehaviour.Instantiate<CinemachineSmoothPath>(trackPrefabRef, Vector3.zero, Quaternion.identity, _dollyTracksManager.transform);
            DollyTrack newDollyTrack = trackPrefabRef.GetComponent<DollyTrack>();
            //add new track to manager tracks list
            _dollyTracksManager.TracksList.Add(newDollyTrack);
            //initializes the new track datas
            newDollyTrack.This = trackPrefabRef;
            newDollyTrack.This.m_Waypoints[0].position = _anchoredTrack.m_Waypoints[^1].position;
            newDollyTrack.This.m_Waypoints[1].position = trackPrefabRef.m_Waypoints[0].position + new Vector3(0f, 0f, 10f);
            newDollyTrack.ID = _dollyTracksManager.TracksList.Count;
            newDollyTrack.AnchoredTrack = _anchoredTrack;
        }
        else
            Debug.Log("Double click the track you want to link the new one");
    }
}
