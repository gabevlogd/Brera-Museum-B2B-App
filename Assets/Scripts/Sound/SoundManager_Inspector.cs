#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SoundManager))]
public class SoundManager_Inspector : Editor
{
    private SoundManager Target;
    private static List<AudioClip> m_AudioClips;
    private static List<string> m_AudioClipsID;
    private AudioClipsData m_AudioClipsData;


    private void Awake()
    {
        Target = target as SoundManager;
        //AudioClipsData audioClipsData = Resources.Load<AudioClipsData>("Audio Clips Datas");
        //if (audioClipsData.AudioClips != null && m_AudioClips == null)
        //{
        //    m_AudioClips = audioClipsData.AudioClips;
        //    m_AudioClipsID = audioClipsData.AudioClipsID;
        //}
        m_AudioClipsData = Resources.Load<AudioClipsData>("Audio Clips Datas");
    }

    public override void OnInspectorGUI()
    {
        if (m_AudioClipsData.AudioClips != null)
        {
            for (int i = 0; i < m_AudioClipsData.AudioClips.Count; i++)
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    m_AudioClipsData.AudioClips[i] = EditorGUILayout.ObjectField(m_AudioClipsData.AudioClips[i], typeof(AudioClip), false) as AudioClip;
                    m_AudioClipsData.AudioClipsID[i] = EditorGUILayout.TextField(m_AudioClipsData.AudioClipsID[i], GUILayout.Width(100f));
                }

            }
        }

        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("+", GUILayout.Width(30f)))
            {
                if (m_AudioClipsData.AudioClips == null)
                {
                    m_AudioClipsData.AudioClips = new List<AudioClip>();
                    m_AudioClipsData.AudioClipsID = new List<string>();
                }

                m_AudioClipsData.AudioClips.Add(null);
                m_AudioClipsData.AudioClipsID.Add(null);
            }
            if (GUILayout.Button("-", GUILayout.Width(30f)) && m_AudioClipsData.AudioClips.Count > 0)
            {
                m_AudioClipsData.AudioClips.RemoveAt(m_AudioClipsData.AudioClips.Count - 1);
                m_AudioClipsData.AudioClipsID.RemoveAt(m_AudioClipsData.AudioClipsID.Count - 1);
            }
        }

        //if (GUILayout.Button("Generate Audio Clips Data"))
        //{
        //    if (m_AudioClips != null && m_AudioClips.Count > 0 && m_AudioClips[0] != null)
        //    {
        //        AudioClipsData audioClipsData = Resources.Load<AudioClipsData>("Audio Clips Datas");
        //        audioClipsData.AudioClips = m_AudioClips;
        //        audioClipsData.AudioClipsID = m_AudioClipsID;
        //    }
            
        //}
            
    }
}
#endif
