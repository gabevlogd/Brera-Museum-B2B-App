using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Pool<AudioSource>
{
    private static SoundManager m_This;

    public AudioClipsData AudioClipsData;
    public Dictionary<string, AudioClip> AudioClips;

    private void Awake()
    {
        m_PoolObjPrefab = Resources.Load<AudioSource>("Audio Source");
        m_This = this;

    }

    public static void Play(AudioClip audioClip)
    {
        AudioSource audioSource = m_This.GetObject();
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
