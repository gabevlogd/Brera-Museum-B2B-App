using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIWindow : MonoBehaviour
{
    protected UIManager m_UIManager;

    protected virtual void Awake() => m_UIManager = GetComponentInParent<UIManager>();

    public void DisableWindow() => gameObject.SetActive(false);
}
