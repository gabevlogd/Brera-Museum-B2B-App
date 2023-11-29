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
    private bool _showTracks;
    private bool _showTrackLinker;

    private DollyTrack _dollyTrackA;
    private DollyTrack _dollyTrackB;



    [MenuItem("Tools/Dolly Tracks Editor")]
    public static void ShowWindow() => GetWindow<DollyTracksEditor>("Dolly Tracks Editor");


    private void Awake()
    {
        _dollyTracksManager = FindObjectOfType<DollyTracksManager>();
    }



    private void OnGUI()
    {

        using (new GUILayout.ScrollViewScope(_scrollPos))
        {
            DrawNewTrackButton();
            DrawUndoButton();
            DrawTracksList();
            DrawTracksLinkerArea();
        }
    }

    private void DrawNewTrackButton()
    {
        GUILayout.Space(20f);
        using (new GUILayout.HorizontalScope())
        {
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("New Track", GUILayout.MaxWidth(200f)))
                PerformNewTrackButton();
            GUILayout.FlexibleSpace();
        }
            
    }

    private void DrawUndoButton()
    {
        using (new GUILayout.HorizontalScope())
        {
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Delete Last Track", GUILayout.MaxWidth(200f)))
                PerformUndoButton();
            GUILayout.FlexibleSpace();
        }
    }

    private void DrawTracksList()
    {
        if (_dollyTracksManager != null)
        {
            _showTracks = EditorGUILayout.Foldout(_showTracks, "Created Tracks");
            if (_showTracks)
            {
                foreach (DollyTrack track in _dollyTracksManager.TracksList)
                {
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        GUILayout.Space(40f);
                        EditorGUILayout.ObjectField(track, typeof(DollyTrack), true);
                    }
                        
                }

                
            }
        }
    }

    private void DrawTracksLinkerArea()
    {
        _showTrackLinker = EditorGUILayout.Foldout(_showTrackLinker, "Tracks Linker");
        if (_showTrackLinker)
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.Space(40f);
                EditorGUILayout.PrefixLabel("Track A");
                _dollyTrackA = EditorGUILayout.ObjectField(_dollyTrackA, typeof(DollyTrack), true) as DollyTrack;
            }
            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.Space(40f);
                EditorGUILayout.PrefixLabel("Track B");
                _dollyTrackB = EditorGUILayout.ObjectField(_dollyTrackB, typeof(DollyTrack), true) as DollyTrack;
            }
            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.Space(40f);
                if (GUILayout.Button("Link A to B") && _dollyTrackA && _dollyTrackB)
                    PerformLinkButton();
            }
                 
        }
    }

    private void PerformNewTrackButton()
    {
        //if dolly manager is null try to find one in the scene
        if (_dollyTracksManager == null)
        {
            _dollyTracksManager = FindObjectOfType<DollyTracksManager>();
        }

        CinemachineSmoothPath trackPrefabRef = Resources.Load<CinemachineSmoothPath>("Dolly Track");

        //if dolly manger is still null or there are no dolly track in scene instantiates the new dolly track without linking it to another track
        if (_dollyTracksManager == null || _dollyTracksManager.TracksList.Count == 0)
        {
            //instantiates the tracks manager if is null
            _dollyTracksManager = (_dollyTracksManager == null) ? _dollyTracksManager = MonoBehaviour.Instantiate<DollyTracksManager>(Resources.Load<DollyTracksManager>("DollyTracksManager")) : _dollyTracksManager;
            //instantiates the new track
            trackPrefabRef = MonoBehaviour.Instantiate<CinemachineSmoothPath>(trackPrefabRef, Vector3.zero, Quaternion.identity, _dollyTracksManager.transform);
            DollyTrack newDollyTrack = trackPrefabRef.GetComponent<DollyTrack>();
            //add new track to manager tracks list
            _dollyTracksManager.TracksList.Add(newDollyTrack);
            //initializes the new track datas
            newDollyTrack.ID = _dollyTracksManager.TracksList.Count;
            newDollyTrack.This = trackPrefabRef;
        }
        //otherwise make sure an in-scene dolly track is selected before creating the new one
        else if (Selection.activeGameObject != null && Selection.activeGameObject.TryGetComponent(out _anchoredTrack))
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
        else Debug.LogError(Constants.DT_TOOL_LOG_ERROR);
    }
    private void PerformUndoButton()
    {
        if (_dollyTracksManager == null || _dollyTracksManager.TracksList.Count == 0) return;
        DollyTrack lastTrack = _dollyTracksManager.TracksList[^1];
        _dollyTracksManager.TracksList.Remove(lastTrack);
        DestroyImmediate(lastTrack.gameObject);
    }

    private void PerformLinkButton()
    {
        //checks if track A is already linked to another
        if (_dollyTrackA.AnchoredTrack != null)
        {
            Debug.LogError("Invalid track. You can't link an already linked track to another track");
            _dollyTrackA = null;
            return;
        }
        //checks if track A is different from track B and if track B is not linked to track A
        else if (_dollyTrackA != _dollyTrackB && _dollyTrackB.AnchoredTrack != _dollyTrackA.This)
        {
            _dollyTrackA.AnchoredTrack = _dollyTrackB.This;
            _dollyTrackA = null;
            _dollyTrackB = null;
        }
        else Debug.LogError("Invalid tracks.");
    }
}
