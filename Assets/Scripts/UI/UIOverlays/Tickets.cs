using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Tickets : UIWindow
{
    [SerializeField]
    private Button m_BackButton;
    [SerializeField]
    private Button m_QuestionsButton;
    [SerializeField]
    private Button m_SettingsButton;
    [SerializeField]
    private Button m_ProfileButton;

    [SerializeField]
    private TextMeshProUGUI m_FirstCodeText;
    [SerializeField]
    private TextMeshProUGUI m_SecondCodeText;

    [SerializeField]
    private Image m_FirstLockIcon;
    [SerializeField]
    private Image m_SecondLockIcon;

    private void OnEnable()
    {
        m_BackButton.onClick.AddListener(CloseWindow);
        m_QuestionsButton.onClick.AddListener(() => m_UIManager.OpenOverlay(Overlay.Questions));
        m_SettingsButton.onClick.AddListener(() => m_UIManager.OpenOverlay(Overlay.Settings));
        m_ProfileButton.onClick.AddListener(() => m_UIManager.OpenOverlay(Overlay.Profile));
        AddSoundFeedback();
        CheckDiscountCodes();
        
    }

    private void OnDisable()
    {
        m_BackButton.onClick.RemoveAllListeners();
        m_QuestionsButton.onClick.RemoveAllListeners();
        m_SettingsButton.onClick.RemoveAllListeners();
        m_ProfileButton.onClick.RemoveAllListeners();
        m_FirstLockIcon.gameObject.SetActive(false);
        m_FirstCodeText.gameObject.SetActive(false);
        m_SecondLockIcon.gameObject.SetActive(false);
        m_SecondCodeText.gameObject.SetActive(false);
        
    }

    private void CheckDiscountCodes()
    {
        if (CanShowDiscount()) m_FirstCodeText.gameObject.SetActive(true);
        else m_FirstLockIcon.gameObject.SetActive(true);

        if (CanShowDiscount() && PlayerPrefs.GetInt(Constants.PUZZLE_FOUR) == 1) m_SecondCodeText.gameObject.SetActive(true);
        else m_SecondLockIcon.gameObject.SetActive(true);
    }

    private void AddSoundFeedback()
    {
        m_BackButton.onClick.AddListener(ButtonSound);
        m_QuestionsButton.onClick.AddListener(ButtonSound);
        m_SettingsButton.onClick.AddListener(ButtonSound);
        m_ProfileButton.onClick.AddListener(ButtonSound);
    }

    private bool CanShowDiscount() => PlayerPrefs.GetInt(Constants.PUZZLE_ONE) == 1 && PlayerPrefs.GetInt(Constants.PUZZLE_TWO) == 1 && PlayerPrefs.GetInt(Constants.PUZZLE_THREE) == 1;
}
