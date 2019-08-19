using System.Collections;
using System.Collections.Generic;
using Gamekit2D;
using UnityEngine;

public class FirstObject : MonoBehaviour
{
    void Start()
    {
        //ScreenFader.SetAlpha(1f);
        StartCoroutine(start());
    }

    IEnumerator start()
    {
        yield return new WaitForSeconds(1f);
        //StartCoroutine (ScreenFader.FadeSceneIn ());
        
        yield return StartCoroutine(ScreenFader.FadeSceneOut(ScreenFader.FadeType.Loading));
        
        
        yield return StartCoroutine(ScreenFader.FadeSceneIn());
    }
}
