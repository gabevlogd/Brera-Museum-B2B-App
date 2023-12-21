using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Questions : UIWindow
{
    [SerializeField]
    private Button m_CloseButton;
    [SerializeField]
    private Button m_OpenPolicyButton;
    [SerializeField]
    private Button m_ClosePolicyButton;
    [SerializeField]
    private GameObject m_PolicyTab;

    private void OnEnable()
    {
        m_CloseButton.onClick.AddListener(CloseWindow);
        m_OpenPolicyButton.onClick.AddListener(() => m_PolicyTab.SetActive(true));
        m_ClosePolicyButton.onClick.AddListener(() => m_PolicyTab.SetActive(false));
        AddSoundFeedback();
    }

    private void OnDisable()
    {
        m_CloseButton.onClick.RemoveAllListeners();
        m_OpenPolicyButton.onClick.RemoveAllListeners();
        m_ClosePolicyButton.onClick.RemoveAllListeners();
    }

    private void AddSoundFeedback()
    {
        m_CloseButton.onClick.AddListener(ButtonSound);
        m_OpenPolicyButton.onClick.AddListener(ButtonSound);
        m_ClosePolicyButton.onClick.AddListener(ButtonSound);
    }
}
