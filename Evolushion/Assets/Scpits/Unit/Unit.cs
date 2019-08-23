using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Unit : MonoBehaviour, TouchTargetedDelegate
{   
    private void Start()
    {    
        GameObject go = GameObject.Find("SharedTouchDispatcher");
        go.GetComponent<TouchDispatcher>().addTargetedDelegate(this, 1, false);
        
        int couis = GameManager.Instance.dataHandler.GoldCount;
        CurrencyUI.Instance.coinText.text = "" + couis;
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
