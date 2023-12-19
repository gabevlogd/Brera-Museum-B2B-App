using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ARManager : MonoBehaviour
{
    [SerializeField] private Camera m_ARCamera;
    public void ImageVisibile(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    //private void OnEnable()
    //{
    //    m_ARCamera.transform.GetChild(0).gameObject.layer = 7;
    //}

    private void Start()
    {
        m_ARCamera.transform.GetChild(0).gameObject.layer = 7;
    }
}
