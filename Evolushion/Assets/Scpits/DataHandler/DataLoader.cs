using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class DataLoader 
{
    #region fields
    
    private DataHandler _dataHandler;
    private GameData gameData;

    #endregion

    #region Methods

    public DataLoader()
    {
        GameManager.Instance.onDataLoaded += SetData;
    }

    public void StartLoad()
    {
        LoadGameSettings();
        LoadUserDataFromXml();
    }

    private DataHandler SetData()
    {
        //TODO if _dataHandler NPE send null object data -  class NullCustomer
        return _dataHandler;
    }
    
    private void LoadUserDataFromXml()
    {
        string path = "Assets\\Resources\\gamedata.xml";
        
        if (File.Exists(path))
        {
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(DataHandler));
                using (FileStream fs = new FileStream("Assets\\Resources\\gamedata.xml", FileMode.OpenOrCreate))
                {
                    _dataHandler = (DataHandler) formatter.Deserialize(fs);
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }
        }

        if (_dataHandler == null)
        {    
            _dataHandler = new DataHandler();
        }
        
        GameManager.Instance.dataHandler = _dataHandler;
        
        GameManager.Instance.gameData.Abdicate();
        GameManager.Instance.gameData.Load(_dataHandler.json);
        GameManager.Instance.gameData.init();
        
        GameManager.Instance.PlayerDataLoaded = true;
    }
    
    private void LoadGameSettings()
    {
        GameSettings.init("data");
    }
    
    public void Save()
    {
        string writer = "Assets\\Resources\\gamedata.xml";

        //clear old data
        FileStream fileStream = File.Open(writer, FileMode.Open);
        fileStream.SetLength(0);
        fileStream.Close();
        
        _dataHandler.json = GameManager.Instance.gameData.Save();
            
        XmlSerializer serializer = new XmlSerializer(typeof(DataHandler));
        FileStream fs = new FileStream(writer, FileMode.OpenOrCreate);
        serializer.Serialize(fs, _dataHandler);
    }

    #endregion
    
}
