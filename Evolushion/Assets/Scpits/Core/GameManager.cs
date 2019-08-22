using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region fields

    public static GameManager Instance;
    public bool IsGameLoading { get; private set; }
    public bool SettingsLoaded{ get; set; }
    public bool PlayerDataLoaded{ get; set; }
    
    private DataLoader _dataLoader;
    public DataHandler dataHandler;
    
    public delegate DataHandler OnDataLoaded();
    public event OnDataLoaded onDataLoaded;
    
    #endregion
    
    #region Methods
    
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
        Screen.orientation = ScreenOrientation.Portrait;
        _dataLoader = new DataLoader();
        _dataLoader = new DataLoader();
        //init services
        
        //load data from settings and player
        LoadData();
    }

    private void LoadMainGameScene()
    {
        SceneManager.LoadScene("Adventure");
    }
    
    private void LoadData()
    {
        IsGameLoading = true;
       
        
        StartCoroutine(SplashSceneLoader.Instance.LoadSavedScene());
        StartCoroutine(GotLoadingResponse());
        _dataLoader.StartLoad();
    }
    
    private IEnumerator GotLoadingResponse()
    {
        //Emulator downloading 2 second
        yield return new WaitForSeconds(2f);
        
        while (!SettingsLoaded && !PlayerDataLoaded)
        {
            yield return new WaitForSeconds(1f);
        }
        
        SplashSceneLoader.Instance.isLoading = false;
        
        LoadMainGameScene();
        yield return null;
    }
    
    private void OnApplicationQuit()
    {
        _dataLoader.Save();
    }
    
    #endregion
}