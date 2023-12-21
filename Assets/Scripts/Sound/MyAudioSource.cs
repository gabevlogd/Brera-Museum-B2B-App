using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAudioSource : MonoBehaviour
{
    public static event Action<AudioSource> AudioClipEnded;
    private AudioSource m_AudioSource;

    private void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!m_AudioSource.isPlaying)
            AudioClipEnded?.Invoke(m_AudioSource);
    }

}
