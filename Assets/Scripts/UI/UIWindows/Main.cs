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
    [SerializeField]
    private Button m_ProfileButton;

    private void OnEnable()
    {
        m_PlayButton.onClick.AddListener(PerformPlayButton);
        m_SettingsButton.onClick.AddListener(() => m_UIManager.OpenOverlay(Overlay.Settings));
        m_FAQButton.onClick.AddListener(() => m_UIManager.OpenOverlay(Overlay.Questions));
        m_TicketsButton.onClick.AddListener(() => m_UIManager.ChangeWindow(Window.Tickets));
        m_ProfileButton.onClick.AddListener(() => m_UIManager.OpenOverlay(Overlay.Profile));
    }

    private void OnDisable()
    {
        m_PlayButton.onClick.RemoveAllListeners();
        m_SettingsButton.onClick.RemoveAllListeners();
        m_FAQButton.onClick.RemoveAllListeners();
        m_TicketsButton.onClick.RemoveAllListeners();
        m_ProfileButton.onClick.RemoveAllListeners();
    }

    private void PerformPlayButton()
    {
        m_UIManager.OpenWindow(Window.HUD);
        m_Animator.Play("Play");
    }

    public void OnPlayAnimationEnded() => m_UIManager.ChangeWindow(Window.HUD);
}
