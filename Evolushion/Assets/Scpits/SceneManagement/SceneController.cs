using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    protected static SceneController instance;
    protected string mDistinationScene;
    protected bool mTransitioning;
    
    public static bool Transitioning
    {
        get { return Instance.mTransitioning; }
    }
    
    void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
    
    public static SceneController Instance
    {
        get
        {
            if (instance != null) return instance;

            instance = FindObjectOfType<SceneController>();

            if (instance != null) return instance;

            Create();

            return instance;
        }
    }
    
    public static SceneController Create()
    {
        GameObject sceneControllerGameObject = new GameObject("SceneController");
        instance = sceneControllerGameObject.AddComponent<SceneController>();

        return instance;
    }
    
    public void TransitionToScene()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name.Equals("Adventure"))
        {
            // SceneManager.GetSceneAt(1).name;
            mDistinationScene = "Ferm";
        }
        else
        {
            mDistinationScene = "Adventure";
        }
        
        Instance.StartCoroutine(Instance.Transition(mDistinationScene));
    }

    protected IEnumerator Transition(string newSceneName)
    {
        mTransitioning = true;

        yield return StartCoroutine(ScreenFader.FadeSceneOut(ScreenFader.FadeType.Loading));
        yield return SceneManager.LoadSceneAsync(newSceneName);
       // SetupNewScene(transitionType, entrance);
        yield return StartCoroutine(ScreenFader.FadeSceneIn());

        mTransitioning = false;
    }

}