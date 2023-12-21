using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class manages the reproduction of sounds
/// </summary>
public class SoundManager : Pool<AudioSource>
{
    private static SoundManager m_This;
    private Dictionary<string, AudioClip> m_AudioClips;
    private static bool m_SoundDisabled;

    private void Awake()
    {
        m_PoolObjPrefab = Resources.Load<AudioSource>("Audio Source");
        m_This = this;
        InitializeManager();
        PlaySountrack();
    }

    private void OnEnable()
    {
        MyAudioSource.AudioClipEnded += StopSource;
        AudioToggle.AudioOn += SoundOn;
        AudioToggle.AudioOn += PlaySountrack;
        AudioToggle.AudioOff += SoundOff;
        AudioToggle.AudioOff += StopSoundtrack;
    }

    private void OnDisable()
    {
        MyAudioSource.AudioClipEnded -= StopSource;
        AudioToggle.AudioOn -= SoundOn;
        AudioToggle.AudioOn -= PlaySountrack;
        AudioToggle.AudioOff -= SoundOff;
        AudioToggle.AudioOff -= StopSoundtrack;
    }

    public static void Play(string audioClipID, bool looped = false)
    {
        if (m_SoundDisabled) return;
        AudioSource audioSource = m_This.GetObject();
        audioSource.clip = m_This.m_AudioClips[audioClipID];
        if (looped) audioSource.loop = true;
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

    private void StopSoundtrack()
    {
        foreach(Transform child in transform)
        {
            if (child.TryGetComponent(out AudioSource audioSource))
            {
                ReleaseObject(audioSource);
            }
        }
    }

    private void PlaySountrack()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            Play("Museum", true);
        else Play("Puzzles", true);
        
    }

    private void SoundOn() => m_SoundDisabled = false;
    private void SoundOff() => m_SoundDisabled = true;
}
