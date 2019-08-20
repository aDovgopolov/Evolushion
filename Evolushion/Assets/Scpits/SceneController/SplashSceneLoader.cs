using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;
using Image = UnityEngine.UI.Image;

public class SplashSceneLoader : MonoBehaviour
{
    #region Fields
    
    [Header("Loading Visuals")]
    public Image[] progressBar;
    public Sprite loadingIcon;
    public Sprite loadingDoneIcon;
    public Image loadingCanvasBackground;
    
    [Header("Timing Settings")]
    public float fadeDuration;
    public bool isLoading;
    public static SplashSceneLoader Instance;

    #endregion

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }
    
    public IEnumerator LoadSavedScene()
    {
        int i = 0;
        
        //shows loading process visuals till data load
        if (GameManager.Instance.IsGameLoading)
        {
            while (!isLoading)
            {
                if (isLoading) break; // when loaded new scene 1 tick NPE
                progressBar[i].sprite = loadingDoneIcon;

                if (i == 0)
                {
                    progressBar[progressBar.Length - 1].sprite = loadingIcon;
                }
                else
                {
                    progressBar[i - 1].sprite = loadingIcon;
                }

                i++;

                if (i == progressBar.Length)
                {
                    i = 0;
                }

                yield return new WaitForSeconds(fadeDuration);
            }
        }
        
        yield return null;
    }
    
}