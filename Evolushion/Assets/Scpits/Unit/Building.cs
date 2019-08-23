using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Building : MonoBehaviour,  MINI.IJSONable
{
    #region MyRegion

    protected string _id = "building";
    protected string _type = "";
    protected bool _isBuilt;
    public UnityEvent m_MyEvent;
    public Sprite _readyBuildimgImage;

    public bool _IsSomethingBuilt { get; private set; }

    #endregion
    
    public string GetID()
    {
        return _id;
    }
    
    public string GetType()
    {
        return _type;
    }
    
    public void SetID(string id)
    {
        _id = id;
    }

    public bool IsBuilt
    {
        get => _isBuilt;
        set => _isBuilt = value;
    }

    public void Init(string id, string type, bool isBuilt)
    {
        _id = id;
        _type = type;
        _isBuilt = isBuilt;
    }

    public static Building Create(string id, string type, bool isBuilt)
    {
        Building boost = new Building();
        boost.Init(id, type, isBuilt);
        return boost;
    }

    public IDictionary ToDict()
    {
        Dictionary<string, object> dict = new Dictionary<string, object>();
        dict["id"] = _id;
        dict["type"] = _type;
        dict["isBuilt"] = _isBuilt;
        return dict;
    }

    public void FromDict(IDictionary dict)
    {
        _id = dict["id"] as string;
        _type = dict["type"].ToString();
        dict["isBuilt"] = _isBuilt;
    
        Init(_id, _type,_isBuilt);
    }
    
    void Start()
    {
        if (m_MyEvent == null)
            m_MyEvent = new UnityEvent();
    }
    
    public void ChangeSprite()
    {
        if (_IsSomethingBuilt) return;
        
        _IsSomethingBuilt = true;
        Animator animator =  GetComponent<Animator>();
        
        Debug.Log($"ChangeSprite() {animator.name}");
        animator.SetTrigger("IsBuilt");
        GetComponent<SpriteRenderer>().sprite = _readyBuildimgImage;
    }

    public void GoToFarmScene()
    {    
        if(_IsSomethingBuilt)
            SceneController.Instance.TransitionToScene();
    }

}