using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private UIWindow m_CurrentWindow;
    private UIWindow m_PreviousWindow;
    private UIWindow m_NextWindow;
    private Dictionary<Window, UIWindow> WindowsList;

    private void Awake()
    {
        InitializeManager();
    }

    private void InitializeManager()
    {
        WindowsList = new Dictionary<Window, UIWindow>()
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
