using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIWindow : MonoBehaviour
{
    protected UIManager m_UIManager;
    protected Animator m_Animator;

    protected virtual void Awake()
    {
        m_UIManager = GetComponentInParent<UIManager>();
        TryGetComponent(out m_Animator);
    }

    public void CloseWindow() => gameObject.SetActive(false);
}
