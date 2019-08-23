using UnityEngine;

public class DataHandler
{   
    private int goldCount;

    public string json;
    
    public int GoldCount 
    { 
        get => goldCount; 
        set => goldCount = value; 
    }
}