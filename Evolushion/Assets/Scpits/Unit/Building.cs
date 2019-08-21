using UnityEngine;
using UnityEngine.Events;

public class Building :MonoBehaviour
{
    public UnityEvent m_MyEvent;
    public Sprite _preparedToBuildImage;
    public Sprite _readyBuildimgImage;
    public Sprite _isBuildingNowImage;
    public bool _IsSomethingBuilt { get; private set; }
    
    void Start()
    {
        if (m_MyEvent == null)
            m_MyEvent = new UnityEvent();
    }

    public void ChangeSprite()
    {
        if (_IsSomethingBuilt) return;
        
        _IsSomethingBuilt = true;
        GetComponent<SpriteRenderer>().sprite = _readyBuildimgImage;
    }

    public void GoToFarmScene()
    {    
        if(_IsSomethingBuilt)
            SceneController.Instance.TransitionToScene();
    }
}