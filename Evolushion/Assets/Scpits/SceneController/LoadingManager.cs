using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class LoadingManager : MonoBehaviour
{
    #region Fields
    
    [Header("Loading Visuals")]
    public Image[] progressBar;
    public Sprite loadingIcon;
    public Sprite loadingDoneIcon;
    public Image loadingCanvasBackground;
    
    [Header("Timing Settings")]
    public float fadeDuration;
    public bool gotLoadingResponse;
    public static LoadingManager Instance;

    #endregion

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);
    }
    
    public IEnumerator LoadSavedScene()
    {
        int i = 0;
        
        //shows loading process visuals till data load
        if (GameManager.Instance.loadingGame)
        {
            while (!gotLoadingResponse)
            {
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

            GameManager.Instance.LoadMainGameScene();
        }
        
        yield return null;
    }
    
}