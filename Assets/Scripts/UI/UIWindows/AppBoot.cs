using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppBoot : UIWindow
{
    [SerializeField]
    [Range(0, 10)]
    private int m_FadeOutDelay;

    private void OnEnable() => StartCoroutine(LifeSpan(m_FadeOutDelay));

    private IEnumerator LifeSpan(int fadeOutDelay)
    {
        yield return new WaitForSeconds(fadeOutDelay);
        m_UIManager.OpenWindow(Window.Main);
        m_Animator.Play("FadeOut");
    }

    public void OnFadeAnimationEnded() => m_UIManager.ChangeWindow(Window.Main);
}
