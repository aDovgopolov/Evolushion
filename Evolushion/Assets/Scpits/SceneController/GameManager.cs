using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector] 
    public static GameManager Instance = null;

    public bool loadingGame;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        loadingGame = true;
        LoadingManager.Instance.gotLoadingResponse = false;
        StartCoroutine(LoadingManager.Instance.LoadSavedScene("Adventure"));
    }

    public void Load()
    {
        loadingGame = false;
        LoadingManager.Instance.gotLoadingResponse = false;
        SceneManager.LoadScene("Adventure");
    }
}