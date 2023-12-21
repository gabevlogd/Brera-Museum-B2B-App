using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Pool<AudioSource>
{
    private static SoundManager m_This;
    private Dictionary<string, AudioClip> m_AudioClips;

    private void Awake()
    {
        m_PoolObjPrefab = Resources.Load<AudioSource>("Audio Source");
        m_This = this;
        InitializeManager();
    }

    private void OnEnable() => MyAudioSource.AudioClipEnded += StopSource;
    private void OnDisable() => MyAudioSource.AudioClipEnded -= StopSource;

    public static void Play(string audioClipID)
    {
        AudioSource audioSource = m_This.GetObject();
        audioSource.clip = m_This.m_AudioClips[audioClipID];
        audioSource.Play();
    }

    private void StopSource(AudioSource audioSource)
    {
        audioSource.clip = null;
        ReleaseObject(audioSource);
    }

    private void InitializeManager()
    {
        int startPoolCount = 4;
        InitializePool(startPoolCount);
        AudioClipsData audioClipsData = Resources.Load<AudioClipsData>("Audio Clips Datas");
        m_AudioClips = new Dictionary<string, AudioClip>();
        for (int i = 0; i < audioClipsData.AudioClips.Count; i++)
        {
            m_AudioClips.Add(audioClipsData.AudioClipsID[i], audioClipsData.AudioClips[i]);
        }
    }
}
