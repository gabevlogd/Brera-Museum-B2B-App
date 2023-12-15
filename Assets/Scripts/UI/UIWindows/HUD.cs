using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : UIWindow
{
    [SerializeField]
    private Button m_MenuButton;
    [SerializeField]
    private Button m_QuestionButton;
    [SerializeField]
    private Button m_ProfileButton;
    [SerializeField]
    private Button m_SettingsButton;
    [SerializeField]
    private Button m_MainMenuButton;

    private bool m_MenuOpen;

    private void OnEnable()
    {
        m_MenuButton.onClick.AddListener(PerformMenuButton);
        m_QuestionButton.onClick.AddListener(() => m_UIManager.OpenOverlay(Overlay.Questions));
        m_ProfileButton.onClick.AddListener(() => m_UIManager.ChangeWindow(Window.Profile, true));
        m_SettingsButton.onClick.AddListener(() => m_UIManager.OpenOverlay(Overlay.Settings));
        m_MainMenuButton.onClick.AddListener(() => m_UIManager.ChangeWindow(Window.Main));
    }

    private void OnDisable()
    {
        m_MenuButton.onClick.RemoveAllListeners();
        m_QuestionButton.onClick.RemoveAllListeners();
        m_ProfileButton.onClick.RemoveAllListeners();
        m_SettingsButton.onClick.RemoveAllListeners();
        m_MainMenuButton.onClick.RemoveAllListeners();
        m_MenuOpen = false;
    }

    private void PerformMenuButton()
    {
        if (!m_MenuOpen)
            m_Animator.Play("OpenMenu");
        else 
            m_Animator.Play("CloseMenu");

        m_MenuOpen = !m_MenuOpen;
    }
}
