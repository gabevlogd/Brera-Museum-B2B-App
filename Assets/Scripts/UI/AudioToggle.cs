using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AudioToggle : MonoBehaviour, IPointerClickHandler
{
    public static event Action AudioOn;
    public static event Action AudioOff;
    private Animator m_Animator;
    private Image m_Image;
    [SerializeField]
    private Sprite m_On;
    [SerializeField]
    private Sprite m_Off;

    private static bool m_IsOff;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_Image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        if (m_IsOff)
            m_Image.sprite = m_Off;
        else m_Image.sprite = m_On;

    }

    public void OnPointerClick(PointerEventData eventData) => TriggerToggle();

    private void TriggerToggle()
    {
        if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.9f) return;

        if (m_IsOff)
        {
            m_Animator.Play("On");
            m_IsOff = false;
            AudioOn?.Invoke();
        }
        else
        {
            m_Animator.Play("Off");
            m_IsOff = true;
            AudioOff?.Invoke();
        }

        SoundManager.Play("Button");
    }
}
