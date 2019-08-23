using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Booster : MINI.IJSONable
{
    protected string _id = "booster";
    protected string _type = "";
    
    public string GetID()
    {
        return _id;
    }

    public void Init(string id, string type)
    {
        _id = id;
        _type = type;
    }

    public static Booster Create(string id, string type)
    {
        Booster boost = new Booster();
        boost.Init(id, type);
        return boost;
    }

    public IDictionary ToDict()
    {
        Dictionary<string, object> dict = new Dictionary<string, object>();
        dict["id"] = _id;
        dict["type"] = _type;
        return dict;
    }

    public void FromDict(IDictionary dict)
    {
        _id = dict["id"] as string;
        _type = dict["type"].ToString();
    
        Init(_id, _type);
    }
}