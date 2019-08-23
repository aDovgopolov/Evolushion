using System.Collections;
using System.Collections.Generic;
using MINI;
using UnityEngine;

public class GameData
{
    #region Fields
    
    private static int version = 1;

    Dictionary<string, Building> _buildings = new Dictionary<string, Building>();
    Dictionary<string, Booster> _boosters = new Dictionary<string, Booster>();

    #endregion
    
    public void init()
    {    
        //test
        Booster boost = new Booster();
        boost.Init("id", "booster");
        AddBooster(boost);
        
        Building Building = new Building();
        Building.Init("ferma", "building1", false);
        AddBuildings(Building);
        
        
        Debug.Log($"{_buildings.Count} + {_boosters.Count}");
    }
    
    #region Methods
    public void Abdicate()
    {
        _buildings.Clear();
        _boosters.Clear();
    }
    
    public void AddBooster(Booster booster)
    {
        var id = booster.GetID();
        if (!_boosters.ContainsKey(id))
            _boosters[id] = booster;
        else
        {
            Debug.LogError("Cant add booster! Booster with same id already exits: " + booster.GetID());
        }
    }
    
    public void AddBuildings(Building building)
    {
        var id = building.GetID();
        if (!_boosters.ContainsKey(id))
            _buildings[id] = building;
        else
        {
            Debug.LogError("Cant add booster! Booster with same id already exits: " + building.GetID());
        }
    }
    
    public IDictionary<string, Booster> GetBoosters()
    {
        return _boosters;
    }
    public IDictionary<string, Building> GetBuildings()
    {
        return _buildings;
    }
    
    public Booster GetBooster(string id)
    {
        if (_boosters.ContainsKey(id))
        {
            var booster =_boosters[id];
            return booster;
        }

        return null;
    }
    
    public Building GetBuilding(string id)
    {
        if (_buildings.ContainsKey(id))
        {
            var building =_buildings[id];
            return building;
        }

        return null;
    }
    
    public bool IsBoosterExists(string id)
    {
        return _boosters.ContainsKey(id);
    }
    
    public bool IsBuildingExists(string id)
    {
        return _buildings.ContainsKey(id);
    }
    
    #endregion
    
    #region Save and Load
    public string Save()
    {
        Dictionary<string, object> dict = new Dictionary<string, object>();

        dict["version"] = version;
        dict["boosters"] = _boosters;
        dict["buildings"] = _buildings;

        return MINI.JSON.Serialize(dict);
    }
    
    public void Load(string textdata)
    {
        var dict = JSON.Deserialize(textdata) as Dictionary<string, object>;
        
        _buildings = JSON.ObjectsFromDictFromFactory<Building>(dict["buildings"] as IDictionary, BuildingFactory.Create);
        _boosters = JSON.ObjectsFromDict<Booster>(dict["boosters"] as IDictionary);
        
        //version = dict.GetValueOrDefault("version", version);
        version = 1;
    }
    
    #endregion
}
