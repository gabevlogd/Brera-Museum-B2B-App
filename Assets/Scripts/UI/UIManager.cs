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
            { Window.AppBoot, GetComponentInChildren<AppBoot>(true) },
            { Window.Main, GetComponentInChildren<Main>(true) },
            { Window.HUD, GetComponentInChildren<HUD>(true) },
            { Window.Tickets, GetComponentInChildren<Tickets>(true) },
            { Window.Settings, GetComponentInChildren<Settings>(true) },
            { Window.Questions, GetComponentInChildren<Questions>(true) },
            { Window.Profile, GetComponentInChildren<Profile>(true) },
        };
    }

    public void ChangeWindow(Window windowID)
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
