#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Cinemachine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;

[CustomEditor(typeof(DollyTrack))]
public class DollyTrack_Inspector : Editor
{
    DollyTrack m_Target;
    AnchorPoint m_FirstAnchorLastValue;
    AnchorPoint m_SecondAnchorLastValue;


    private void OnEnable() => m_Target = target as DollyTrack;

    public override void OnInspectorGUI()
    {
        DrawAnchorPointFields();
        DrawGenerateButtonsButton();
    }


    private void OnDestroy()
    {
        Debug.Log(m_Target);
    }

    private void DrawAnchorPointFields()
    {
        EditorGUI.BeginChangeCheck();
        m_Target.FirstAnchorPoint = EditorGUILayout.ObjectField("First Anchor Point", m_Target.FirstAnchorPoint, typeof(AnchorPoint), true) as AnchorPoint;
        m_Target.SecondAnchorPoint = EditorGUILayout.ObjectField("Second Anchor Point", m_Target.SecondAnchorPoint, typeof(AnchorPoint), true) as AnchorPoint;
        if (EditorGUI.EndChangeCheck())
        {
            CheckForAnchorPointChanges();
        }
        m_FirstAnchorLastValue = m_Target.FirstAnchorPoint;
        m_SecondAnchorLastValue = m_Target.SecondAnchorPoint;
    }

    private void DrawGenerateButtonsButton()
    {
        if (GUILayout.Button("Generate Buttons"))
            PerformGenerateButtons();
    }

    private void DrawNewTrackButton()
    {
        if (GUILayout.Button("New Track"))
            PerformNewTrackButton();
    }

    private void CheckForAnchorPointChanges()
    {
        Debug.Log(m_FirstAnchorLastValue);
        if (m_Target.FirstAnchorPoint == null && m_FirstAnchorLastValue != null)
        {
            m_FirstAnchorLastValue.ButtonsList.Remove(m_Target.SecondButton);
        }
        else if (m_FirstAnchorLastValue != null)
        {
            m_FirstAnchorLastValue.ButtonsList.Remove(m_Target.SecondButton);
            m_Target.FirstAnchorPoint.ButtonsList.Add(m_Target.SecondButton);
        }
        else if (m_Target.FirstAnchorPoint != null)
        {
            m_Target.FirstAnchorPoint.ButtonsList.Add(m_Target.SecondButton);
        }

        if (m_Target.SecondAnchorPoint == null && m_SecondAnchorLastValue != null)
        {
            m_SecondAnchorLastValue.ButtonsList.Remove(m_Target.FirstButton);
        }
        else if (m_SecondAnchorLastValue != null)
        {
            m_SecondAnchorLastValue.ButtonsList.Remove(m_Target.FirstButton);
            m_Target.SecondAnchorPoint.ButtonsList.Add(m_Target.FirstButton);
        }
        else if (m_Target.SecondAnchorPoint != null)
        {
            m_Target.SecondAnchorPoint.ButtonsList.Add(m_Target.FirstButton);
        }


    }

    private void PerformNewTrackButton()
    {
        DollyTrack newDollyAssetRef = Resources.Load<DollyTrack>("Dolly Track");
        DollyTrack newDollyInstanceRef = Instantiate(newDollyAssetRef);

        newDollyInstanceRef.This = newDollyInstanceRef.GetComponent<CinemachineSmoothPath>();
        newDollyInstanceRef.FirstAnchorPoint = m_Target.SecondAnchorPoint;
        newDollyInstanceRef.This.m_Waypoints[1].position = newDollyInstanceRef.FirstAnchorPoint.transform.position + new Vector3(0f, 0f, 10f);
    }

    private void PerformGenerateButtons()
    {
        if (m_Target.FirstButton != null || m_Target.SecondButton != null) return;

        m_Target.FirstButton = Instantiate(Resources.Load<MoveButton>("Move Button"), m_Target.This.m_Waypoints[0].position + Vector3.up * 3f, Quaternion.identity, m_Target.transform);
        m_Target.SecondButton = Instantiate(Resources.Load<MoveButton>("Move Button"), m_Target.This.m_Waypoints[^1].position + Vector3.up * 3f, Quaternion.identity, m_Target.transform);
        m_Target.FirstButton.Track = m_Target.This;
        m_Target.FirstButton.Direction = TrackDirection.Backward;
        m_Target.SecondButton.Track = m_Target.This;
        m_Target.SecondButton.Direction = TrackDirection.Forward;
    }

    [MenuItem("GameObject/Custom GameObject/Dolly Track")]
    public static void CreateDollyTrack()
    {
        DollyTrack trackPrefabRef = Resources.Load<DollyTrack>("Dolly Track");
        DollyTrack newDolly = Instantiate(trackPrefabRef);
        newDolly.This = newDolly.GetComponent<CinemachineSmoothPath>();

        //newDolly.FirstButton = Instantiate(Resources.Load<MoveButton>("Move Button"), newDolly.This.m_Waypoints[0].position + Vector3.up * 3f, Quaternion.identity, newDolly.transform);
        //newDolly.SecondButton = Instantiate(Resources.Load<MoveButton>("Move Button"), newDolly.This.m_Waypoints[^1].position + Vector3.up * 3f, Quaternion.identity, newDolly.transform);
        //newDolly.FirstButton.Track = newDolly.This;
        //newDolly.FirstButton.Direction = TrackDirection.Backward;
        //newDolly.SecondButton.Track = newDolly.This;
        //newDolly.SecondButton.Direction = TrackDirection.Forward;
    }
}

#endif
