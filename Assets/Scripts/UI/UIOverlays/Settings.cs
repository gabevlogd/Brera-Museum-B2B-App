using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : UIWindow
{
    [SerializeField]
    private Button m_BackButton;
    [SerializeField]
    private Button m_QuestionsButton;
    [SerializeField]
    private Button m_ProfileButton;

    private void OnEnable()
    {
        m_BackButton.onClick.AddListener(CloseWindow);
        m_QuestionsButton.onClick.AddListener(() => m_UIManager.OpenOverlay(Overlay.Questions));
        m_ProfileButton.onClick.AddListener(() => m_UIManager.OpenOverlay(Overlay.Profile));
    }

    private void OnDisable()
    {
        m_BackButton.onClick.RemoveAllListeners();
        m_QuestionsButton.onClick.RemoveAllListeners();
        m_ProfileButton.onClick.RemoveAllListeners();
    }

}
