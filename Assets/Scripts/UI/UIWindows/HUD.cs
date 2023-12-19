using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : UIWindow
{
    public static event Func<bool> CanOpenMenu;
    public static event Action OnMenuOpen;
    public static event Action OnMenuClose;

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
        OnMenuClose?.Invoke(); //trigger the change state of player (name of action have no sense here but it is what it is)
        m_MenuButton.onClick.AddListener(PerformMenuButton);
        m_QuestionButton.onClick.AddListener(() => m_UIManager.OpenOverlay(Overlay.Questions));
        m_ProfileButton.onClick.AddListener(() => m_UIManager.OpenOverlay(Overlay.Profile));
        m_SettingsButton.onClick.AddListener(() => m_UIManager.OpenOverlay(Overlay.Settings));
        m_MainMenuButton.onClick.AddListener(() => m_UIManager.ChangeWindow(Window.Main));
        CanOpenMenu = (CanOpenMenu == null) ? () => true : CanOpenMenu; //need to test validity of this line
        m_WindowType = Window.HUD;
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

    private void OnDestroy() => CanOpenMenu = null; //need to test validity of this line

    private void PerformMenuButton()
    {
        //avoid the spam of the menu button 
        if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.9f) return;

        if (!m_MenuOpen && CanOpenMenu())
            OpenMenu();
        else if (m_MenuOpen)
            CloseMenu();
    }

    private void OpenMenu()
    {
        OnMenuOpen?.Invoke();
        m_Animator.Play("OpenMenu");
        m_MenuOpen = true;
    }

    private void CloseMenu()
    {
        OnMenuClose?.Invoke();
        m_Animator.Play("CloseMenu");
        m_MenuOpen = false;
    }
}
