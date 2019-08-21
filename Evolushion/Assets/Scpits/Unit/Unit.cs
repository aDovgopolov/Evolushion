using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Unit : MonoBehaviour, TouchTargetedDelegate
{    
    [Serializable]
    public class HealEvent : UnityEvent<int, int>
    { }
    public HealEvent OnGainHealth;
    
    
    private Text text;
    private void Start()
    {    
        //OnGainHealth = new HealEvent();
        Debug.Log(OnGainHealth);
        OnGainHealth.AddListener(checkX);
        OnGainHealth.Invoke(5,5);
        
        foreach (var unit in GameSettings.unit.items)
        {
            Debug.Log($"{unit.gold_amount} + {unit.id}" );
        }
        GameObject go = GameObject.Find("SharedTouchDispatcher");
        go.GetComponent<TouchDispatcher>().addTargetedDelegate(this, 1, false);
        
        CurrencyUI.Instance.coinText.text = "" + GameManager.Instance.dataHandler.GoldCount;
    }

    public void checkX(int x, int y)
    {
       // 
    }
    
    public bool TouchBegan(Vector2 position, int fingerId)
    {
        return true;
    }

    public void TouchMoved(Vector2 position, int fingerId)
    {
        
        Vector3 vec = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, 10f));
        transform.position = vec;
        GameManager.Instance.dataHandler.GoldCount += 1;
        CurrencyUI.Instance.coinText.text = "" + GameManager.Instance.dataHandler.GoldCount;
    }

    public void TouchEnded(Vector2 position, int fingerId)
    {
    }

    public void TouchCanceled(Vector2 position, int fingerId)
    {
    }
    
}
