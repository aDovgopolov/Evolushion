using System.Collections.Generic;
using UnityEngine;


public class DataHandler
{   
    [SerializeField]
    private int goldCount;

    public string json;
    public int manaCount = 50;
    
    private Dictionary<int, string> countries = new Dictionary<int, string>(5);
    
    public int GoldCount 
    { 
        get => goldCount; 
        set => goldCount = value; 
    }

    public void init()
    {
        countries.Add(1, "Russia");
        countries.Add(3, "Great Britain");
        countries.Add(2, "USA");
        countries.Add(4, "France");
        countries.Add(5, "China");   
        
    }

    public void showData()
    {
        foreach (KeyValuePair<int, string> keyValue in countries)
        {
            Debug.Log(keyValue.Key + " - " + keyValue.Value);
        }
    }
}