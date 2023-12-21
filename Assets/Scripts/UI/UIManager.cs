using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class manages all the windows and overlays that composes the UI
/// </summary>
public class UIManager : MonoBehaviour
{
    public bool GameAlreadyStarted;
    private static int m_LastWindow;
    private UIWindow m_CurrentWindow;
    private UIWindow m_PreviousWindow;
    private Dictionary<Window, UIWindow> m_WindowsList;
    private Dictionary<Overlay, UIWindow> m_OverlaysList;

    private void Awake() => InitializeManager();

    private void OnDisable() => m_LastWindow = (int)m_CurrentWindow.m_WindowType;

    private void InitializeManager()
    {
        m_WindowsList = new Dictionary<Window, UIWindow>()
        {
            { Window.AppBoot, GetComponentInChildren<AppBoot>(true) },
            { Window.Main, GetComponentInChildren<Main>(true) },
            { Window.HUD, GetComponentInChildren<HUD>(true) }
        };
        m_OverlaysList = new Dictionary<Overlay, UIWindow>()
        {
            { Overlay.Questions, GetComponentInChildren<Questions>(true) },
            { Overlay.Settings, GetComponentInChildren<Settings>(true) },
            { Overlay.Profile, GetComponentInChildren<Profile>(true) },
            { Overlay.Tickets, GetComponentInChildren<Tickets>(true) }
        };

        //Check which ui need to be opened (if the first one at the start of game or the last one cached)
        if (m_LastWindow != 0) m_CurrentWindow = m_WindowsList[(Window)m_LastWindow];
        else
        {
            m_CurrentWindow = m_WindowsList[Window.AppBoot];
            GameAlreadyStarted = true;
        }

        m_CurrentWindow.gameObject.SetActive(true);
    }

    public void OpenWindow(Window windowID) => m_WindowsList[windowID].gameObject.SetActive(true);
    public void OpenOverlay(Overlay overlayID)
    {
        UIWindow overlay = m_OverlaysList[overlayID];
        overlay.gameObject.SetActive(true);
        overlay.transform.SetAsLastSibling();
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
    HUD
}

public enum Overlay
{
    Questions,
    Settings,
    Profile,
    Tickets
}

