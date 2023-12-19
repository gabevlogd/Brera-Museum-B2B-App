using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Profile : UIWindow
{
    [SerializeField]
    private Button m_BackButton;
    [SerializeField]
    private Button m_QuestionsButton;
    [SerializeField]
    private Button m_SettingsButton;

    private void OnEnable()
    {
        m_BackButton.onClick.AddListener(CloseWindow);
        m_QuestionsButton.onClick.AddListener(() => m_UIManager.OpenOverlay(Overlay.Questions));
        m_SettingsButton.onClick.AddListener(() => m_UIManager.OpenOverlay(Overlay.Settings));
    }

    private void OnDisable()
    {
        m_BackButton.onClick.RemoveAllListeners();
        m_QuestionsButton.onClick.RemoveAllListeners();
        m_SettingsButton.onClick.RemoveAllListeners();
    }

}
