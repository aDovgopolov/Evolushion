using System;
using UnityEngine;

public class Unit : MonoBehaviour, TouchTargetedDelegate
{
    private void Start()
    {

        foreach (var unit in Data.unit.items)
        {
            Debug.Log($"{unit.gold_amount} + {unit.id}" );
        }
        GameObject go = GameObject.Find("SharedTouchDispatcher");
        go.GetComponent<TouchDispatcher>().addTargetedDelegate(this, 1, false);
    }

    public bool TouchBegan(Vector2 position, int fingerId)
    {
        return true;
    }

    public void TouchMoved(Vector2 position, int fingerId)
    {
        Vector3 vec = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, 10f));
        transform.position = vec;
    }

    public void TouchEnded(Vector2 position, int fingerId)
    {
    }

    public void TouchCanceled(Vector2 position, int fingerId)
    {
    }
    
}
