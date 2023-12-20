using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : UIWindow
{
    public static event Func<bool> CanOpenMenu;
    public static event Action OnMenuOpen;
    public static event Action OnMenuClose;
    public static event Action FirstHUDOpening;
    public static event Action PictureInfoClosed;

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
    [SerializeField]
    private Button m_NotificationButton;
    [SerializeField]
    private Button m_ClosePictureInfoButton;

    [SerializeField]
    private TextMeshProUGUI m_TemporaryMessage;
    [SerializeField]
    private TextMeshProUGUI m_PictureInfoText;

    [SerializeField]
    private GameObject m_PictureInfo;
    [SerializeField]
    private float MessageFadeOutSpeed;

    private bool m_MenuOpen;
    private bool m_PictureInfoOpen;

    private void Start()
    {
        Debug.Log("HUD START");
        if (m_UIManager.GameAlreadyStarted)
        {
            m_UIManager.GameAlreadyStarted = false;
            FirstHUDOpening?.Invoke();
        }
    }

    private void OnEnable()
    {
        if (!m_UIManager.GameAlreadyStarted)
            OnMenuClose?.Invoke(); //trigger the change state of player (name of action have no sense here but it is what it is)
        m_MenuButton.onClick.AddListener(PerformMenuButton);
        m_QuestionButton.onClick.AddListener(() => m_UIManager.OpenOverlay(Overlay.Questions));
        m_ProfileButton.onClick.AddListener(() => m_UIManager.OpenOverlay(Overlay.Profile));
        m_SettingsButton.onClick.AddListener(() => m_UIManager.OpenOverlay(Overlay.Settings));
        m_MainMenuButton.onClick.AddListener(() => m_UIManager.ChangeWindow(Window.Main));
        m_NotificationButton.onClick.AddListener(PerformNotificationButton);
        m_ClosePictureInfoButton.onClick.AddListener(HidePictureInfo);
        RoomLocker.RoomLockedMessage += ThrowScreenMessage;
        RoomLocker.RoomUnlocked += ThrowScreenNotification;
        ARTrigger.LastPuzzleCompleted += ThrowScreenNotification;
        PictureInfoTrigger.OpenPictureInfo += ShowPictureInfo;
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
        m_NotificationButton.onClick.RemoveAllListeners();
        m_ClosePictureInfoButton.onClick.RemoveAllListeners();
        RoomLocker.RoomLockedMessage -= ThrowScreenMessage;
        RoomLocker.RoomUnlocked -= ThrowScreenNotification;
        ARTrigger.LastPuzzleCompleted -= ThrowScreenNotification;
        PictureInfoTrigger.OpenPictureInfo -= ShowPictureInfo;
        m_MenuOpen = false;
    }

    private void OnDestroy() => CanOpenMenu = null; //need to test validity of this line

    private void PerformMenuButton()
    {
        //avoid the spam of the menu button 
        if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.9f) return;
        if (m_PictureInfoOpen) return;

        if (!m_MenuOpen && CanOpenMenu())
            OpenMenu();
        else if (m_MenuOpen)
            CloseMenu();
    }

    private void PerformNotificationButton()
    {
        OpenMenu();
        m_UIManager.OpenOverlay(Overlay.Tickets);
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

    private void ThrowScreenNotification() => m_Animator.Play("Notification");

    private void ThrowScreenMessage() => StartCoroutine(TemporaryMessage("Completa prima tutti i Puzzle", 2f));

    private void ShowPictureInfo(string infoText)
    {
        OnMenuOpen?.Invoke();
        m_PictureInfoOpen = true;
        m_PictureInfoText.text = infoText;
        m_PictureInfo.SetActive(true);
    }

    private void HidePictureInfo()
    {
        OnMenuClose?.Invoke();
        m_PictureInfoOpen = false;
        PictureInfoClosed?.Invoke();
        m_PictureInfo.SetActive(false);
    }


    private IEnumerator TemporaryMessage(string msg, float lifeSpan)
    {
        m_TemporaryMessage.text = msg;
        m_TemporaryMessage.color = new Color(m_TemporaryMessage.color.r, m_TemporaryMessage.color.g, m_TemporaryMessage.color.b, 1f);

        yield return new WaitForSeconds(lifeSpan);

        Color textColor = m_TemporaryMessage.color;
        while (m_TemporaryMessage.color.a > 0)
        {
            textColor.a -= Time.deltaTime * MessageFadeOutSpeed;
            m_TemporaryMessage.color = textColor;
            yield return null;
        }
    }
}
