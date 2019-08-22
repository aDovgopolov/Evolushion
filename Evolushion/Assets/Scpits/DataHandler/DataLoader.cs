using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class DataLoader 
{
    private DataHandler _dataHandler;
    private GameData gameData;
    public DataLoader()
    {
        gameData = new GameData();
        GameManager.Instance.onDataLoaded += SetData;
    }

    public void StartLoad()
    {
        // Start load settings
        LoadGameSettings();
        //Start load person data
        LoadUserDataFromXml();
        //Start load settings
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
                    //empty file can be
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
            // init null strategy
            _dataHandler = new DataHandler();
        }
        
         gameData.Load(_dataHandler.json);
        _dataHandler.init();
        
        GameManager.Instance.dataHandler = _dataHandler;
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
        
        _dataHandler.json = gameData.Save();
            
        _dataHandler.showData();
        XmlSerializer serializer = new XmlSerializer(typeof(DataHandler));
        FileStream fs = new FileStream(writer, FileMode.OpenOrCreate);
        serializer.Serialize(fs, _dataHandler);
    }
}
