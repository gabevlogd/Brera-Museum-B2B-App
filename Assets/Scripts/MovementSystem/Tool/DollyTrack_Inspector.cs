#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Cinemachine;
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
        DrawDeleteButton();
    }


    private void DrawAnchorPointFields()
    {
        EditorGUI.BeginChangeCheck();
        m_Target.FirstAnchorPoint = EditorGUILayout.ObjectField("First Anchor Point", m_Target.FirstAnchorPoint, typeof(AnchorPoint), true) as AnchorPoint;
        m_Target.LastAnchorPoint = EditorGUILayout.ObjectField("Second Anchor Point", m_Target.LastAnchorPoint, typeof(AnchorPoint), true) as AnchorPoint;
        if (EditorGUI.EndChangeCheck())
        {
            CheckForAnchorPointChanges();
        }
        m_FirstAnchorLastValue = m_Target.FirstAnchorPoint;
        m_SecondAnchorLastValue = m_Target.LastAnchorPoint;
    }

    private void DrawDeleteButton()
    {
        if (GUILayout.Button("Delete Dolly Track"))
            PerformDeleteButton();
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

        if (m_Target.LastAnchorPoint == null && m_SecondAnchorLastValue != null)
        {
            m_SecondAnchorLastValue.ButtonsList.Remove(m_Target.FirstButton);
        }
        else if (m_SecondAnchorLastValue != null)
        {
            m_SecondAnchorLastValue.ButtonsList.Remove(m_Target.FirstButton);
            m_Target.LastAnchorPoint.ButtonsList.Add(m_Target.FirstButton);
        }
        else if (m_Target.LastAnchorPoint != null)
        {
            m_Target.LastAnchorPoint.ButtonsList.Add(m_Target.FirstButton);
        }


    }


    private void PerformDeleteButton()
    {
        if (m_Target.FirstAnchorPoint != null && m_Target.FirstAnchorPoint.ButtonsList.Contains(m_Target.SecondButton))
            m_Target.FirstAnchorPoint.ButtonsList.Remove(m_Target.SecondButton);

        if (m_Target.LastAnchorPoint != null && m_Target.LastAnchorPoint.ButtonsList.Contains(m_Target.FirstButton))
            m_Target.LastAnchorPoint.ButtonsList.Remove(m_Target.FirstButton);

        DestroyImmediate(m_Target.gameObject);
    }

    [MenuItem("GameObject/Custom GameObject/Dolly Track")]
    public static void CreateDollyTrack()
    {
        DollyTrack trackPrefabRef = Resources.Load<DollyTrack>("Dolly Track");
        DollyTrack newDolly = Instantiate(trackPrefabRef);
        newDolly.This = newDolly.GetComponent<CinemachineSmoothPath>();

        newDolly.FirstButton = Instantiate(Resources.Load<MoveButton>("Move Button"), newDolly.This.m_Waypoints[0].position + Vector3.up * 3f, Quaternion.identity, newDolly.transform);
        newDolly.SecondButton = Instantiate(Resources.Load<MoveButton>("Move Button"), newDolly.This.m_Waypoints[^1].position + Vector3.up * 3f, Quaternion.identity, newDolly.transform);
        newDolly.FirstButton.Track = newDolly.This;
        newDolly.FirstButton.Direction = TrackDirection.Backward;
        newDolly.SecondButton.Track = newDolly.This;
        newDolly.SecondButton.Direction = TrackDirection.Forward;
    }
}

#endif
