using System;
using System.IO;
using System.Xml.Serialization;

[System.Serializable]
public class DataHandler
{    
    private int goldCount;
    public int GoldCount 
    { 
        get => goldCount; 
        set => goldCount = value; 
    }
    
}