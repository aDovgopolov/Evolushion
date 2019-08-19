using UnityEngine;
using System.Collections;
using Gamekit2D;
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

        DontDestroyOnLoad(gameObject);
    }

	public IEnumerator GotLoadingResponse()
	{
        //Emulator downloading
		yield return new WaitForSeconds(5f);
        gotLoadingResponse = true;
		yield return null;
        Debug.Log($"public IEnumerator GotLoadingResponse()");
	}

	public IEnumerator LoadSavedScene(string sceneName)
    {
		StartCoroutine(GotLoadingResponse());

        /*if (currentScene.name != loadingScene)
        {
            SceneManager.LoadScene(loadingScene);
        }
        */

        int i = 0;
        Debug.Log($"{GameManager.Instance.loadingGame}");
        //shows loading process visuals till data load
        if (GameManager.Instance.loadingGame)
        {
            while (!gotLoadingResponse)
            {
                Debug.Log($"while (!gotLoadingResponse) {gotLoadingResponse} ");
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

            GameManager.Instance.Load();
        }
        
        yield return null;
    }
    
}