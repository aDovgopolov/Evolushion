using System;
using System.Collections;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region fields

    [HideInInspector]
    public static GameManager Instance;
    public bool loadingGame;
    private AllData _allData;
    #endregion
    
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
        LoadData();
    }

    public void LoadMainGameScene()
    {
        LoadingManager.Instance.gotLoadingResponse = false;
        
        _allData.GoldCount = Data.general.gold.start_count;
        SceneManager.LoadScene("Adventure");
        
    }
    
    public IEnumerator GotLoadingResponse()
    {
        Data.init("data");
        
        //Emulator downloading 1 second
        yield return new WaitForSeconds(3f);
        LoadingManager.Instance.gotLoadingResponse = true;
        yield return null;
    }
    
    private void Save()
    {
        string writer = "Assets\\Resources\\gamedata.xml";

        FileStream fileStream = File.Open(writer, FileMode.Open);
        fileStream.SetLength(0);
        fileStream.Close();

        XmlSerializer serializer = new XmlSerializer(typeof(AllData));
        FileStream fs = new FileStream(writer, FileMode.OpenOrCreate);

        serializer.Serialize(fs, _allData);
    }
    
    private void LoadData()
    {
        loadingGame = true;
        
        //Start UI loading
        StartCoroutine(LoadingManager.Instance.LoadSavedScene());
        //Start load settings
        StartCoroutine(GotLoadingResponse());
        //Start load person data
        Load();
    }
    
    private void Load()
    {
        string path = "Assets\\Resources\\gamedata.xml";
        if (File.Exists(path))
        {
            XmlSerializer formatter = new XmlSerializer(typeof(AllData));
            using (FileStream fs = new FileStream("Assets\\Resources\\gamedata.xml", FileMode.OpenOrCreate))
            {
                _allData = (AllData) formatter.Deserialize(fs);
            }
        }
        else
        {
            _allData = new AllData();
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}