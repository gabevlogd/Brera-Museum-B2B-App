using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private UIWindow m_CurrentWindow;
    private UIWindow m_PreviousWindow;
    private Dictionary<Window, UIWindow> m_WindowsList;

    private void Awake()
    {
        InitializeManager();
    }

    private void InitializeManager()
    {
        m_WindowsList = new Dictionary<Window, UIWindow>()
        {
            { Window.AppBoot, GetComponentInChildren<AppBoot>() },
            { Window.Main, GetComponentInChildren<Main>() },
            { Window.HUD, GetComponentInChildren<HUD>() },
            { Window.Tickets, GetComponentInChildren<Tickets>() },
            { Window.Settings, GetComponentInChildren<Settings>() },
            { Window.Questions, GetComponentInChildren<Questions>() },
            { Window.Profile, GetComponentInChildren<Profile>() },
        };
    }

    private void ChangeWindow(Window windowID)
    {
        m_CurrentWindow.gameObject.SetActive(false);
        m_PreviousWindow = m_CurrentWindow;
        m_CurrentWindow = m_WindowsList[windowID];
        m_CurrentWindow.gameObject.SetActive(true);
    }
}

public enum Window
{
    AppBoot,
    Main,
    HUD,
    Tickets,
    Settings,
    Questions,
    Profile
}
