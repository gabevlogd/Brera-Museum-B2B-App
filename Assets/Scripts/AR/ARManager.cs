using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ARManager : MonoBehaviour
{
    public void ImageVisibile(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
