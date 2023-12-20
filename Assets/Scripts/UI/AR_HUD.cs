using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AR_HUD : MonoBehaviour
{
    public static event Action OnCloseAR_HUD;
    [SerializeField]
    private Button m_BackButton;
    [SerializeField]
    private GameObject ARSystem;

    private void OnEnable() => m_BackButton.onClick.AddListener(CloseAR);

    private void OnDisable() => m_BackButton.onClick.RemoveAllListeners();

    private void CloseAR()
    {
        OnCloseAR_HUD?.Invoke();
        ARSystem.SetActive(false);
    }
}
