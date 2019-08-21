using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingleButtonTimeless : MonoBehaviour
{
    public void LoadFarmScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        StartCoroutine(ScreenFader.FadeSceneOut(ScreenFader.FadeType.Loading));
        StartCoroutine(ScreenFader.FadeSceneIn());
        
        if (scene.name.Equals("Adventure"))
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }
}
