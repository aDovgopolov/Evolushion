using System.Collections;
using System.Collections.Generic;
using MINI;
using UnityEngine;

public class GameData
{
    private static int version = 1;

    Dictionary<string, Building> _buildings = new Dictionary<string, Building>();
    
    public string Save()
    {    
        Building Building = new Building();
        Building.SetID("sdasdads");
        Building.Duration = 100;
        Building Building2 = new Building();
        Building2.SetID("asdasdasdasd");
        Building2.Duration = 500;
        _buildings["sdasdads"] = Building;
        _buildings["asdasdasdasd"] = Building2;
        Debug.Log($"Save() + {_buildings.Count}" );
        Dictionary<string, object> dict = new Dictionary<string, object>();

        dict["version"] = version;
        dict["spells"] = _buildings;

        return MINI.JSON.Serialize(dict);
    }

    public void Load(string textdata)
    {
        var dict = MINI.JSON.Deserialize(textdata) as Dictionary<string, object>;

        
        _buildings = MINI.JSON.ObjectsFromDictFromFactory<Building>(dict["spells"] as IDictionary, BuildingFactory.Create);
        
        Debug.Log($"Save() + {_buildings["sdasdads"]} + {_buildings.Count} + {_buildings["sdasdads"].Duration}" );
        //version = dict.GetValueOrDefault("version", version);
        version = 1;
    }

}
