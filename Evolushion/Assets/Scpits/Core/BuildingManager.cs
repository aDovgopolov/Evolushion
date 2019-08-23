using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    #region Fields
    public static BuildingManager instance;

    public int millPrice = 100;

    [SerializeField]
    private List<GameObject> _buildList = new List<GameObject>();
    #endregion
    
    #region Methods
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SetBuildingsOnMap();
    }

    public void SetBuildigIsBuild()
    {
        _buildList[0].GetComponent<Building>().ChangeSprite();
       // _buildList[0].GetComponent<Building>().ChangeSprite();
    }

    public void SetBuildingsOnMap()
    {
            Debug.Log(GameManager.Instance.gameData.GetBuildings().Count);
            
            foreach (KeyValuePair<string, Building> keyValue in GameManager.Instance.gameData.GetBuildings())
            {
                Debug.Log(keyValue.Key + " - " + keyValue.Value.GetID() + keyValue.Value.GetType());
                if (keyValue.Value.GetID().Equals("ferma") && keyValue.Value.IsBuilt)
                {
                    _buildList[0].GetComponent<Building>().ChangeSprite();
                    keyValue.Value.IsBuilt = true;
                }
            }
            
    }
    #endregion
}