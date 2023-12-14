using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Main : UIWindow
{
    [SerializeField]
    private Button m_PlayButton;
    [SerializeField]
    private Button m_SettingsButton;
    [SerializeField]
    private Button m_FAQButton;
    [SerializeField]
    private Button m_TicketsButton;

    private Animator m_Animator;

    protected override void Awake()
    {
        base.Awake();
        m_Animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        m_PlayButton.onClick.AddListener(() => m_Animator.Play("Play"));
        m_SettingsButton.onClick.AddListener(() => m_UIManager.ChangeWindow(Window.Settings));
        m_FAQButton.onClick.AddListener(() => m_UIManager.ChangeWindow(Window.Questions));
        m_TicketsButton.onClick.AddListener(() => m_UIManager.ChangeWindow(Window.Tickets));
    }

    private void OnDisable()
    {
        m_PlayButton.onClick.RemoveAllListeners();
        m_SettingsButton.onClick.RemoveAllListeners();
        m_FAQButton.onClick.RemoveAllListeners();
        m_TicketsButton.onClick.RemoveAllListeners();
    }
}
