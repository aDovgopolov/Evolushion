using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Building :MonoBehaviour
{
    #region MyRegion

    protected string _id = "booster";
    protected string _type = "";
    protected string _icon = "";
    protected float _affectValue = 0.0f;
    protected long _duration = 0;
    protected long _leftTime = 0;
    protected bool _ended = true;
    protected long _starterAt = 0;
    protected List<string> _affects = new List<string>();
    
    
    public UnityEvent m_MyEvent;
    public Sprite _preparedToBuildImage;
    public Sprite _readyBuildimgImage;
    public Sprite _isBuildingNowImage;
    public bool _IsSomethingBuilt { get; private set; }

    #endregion


    public long Duration
    {
        get => _duration;
        set => _duration = value;
    }

    public string GetID()
    {
        return _id;
    }

    public void SetID(string id)
    {
        _id = id;
    }
    void Start()
    {
        if (m_MyEvent == null)
            m_MyEvent = new UnityEvent();
    }

    public void Init(string id)
    {
        
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