using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region fields

    public static GameManager Instance;
    public bool IsGameLoading { get; private set; } = false;
    public bool SettingsLoaded{ get; set; } = false;
    public bool PlayerDataLoaded{ get; set; } = false;
    
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
        
        //here init services
        
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
        
        //Start UI loading
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
        // Set loaded data. Kostul? 
        //dataHandler = onDataLoaded?.Invoke();
        //stop loading UI
        SplashSceneLoader.Instance.isLoading = false;
        
        LoadMainGameScene();
        yield return null;
    }
    
    private void OnApplicationQuit()
    {
        _dataLoader.Save();
    }
    
    #endregion

    #region afterRefactoring
    

    #endregion
}