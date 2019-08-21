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
    
    //vremenniu kostul dlya testa
    public void SetBuildigIsBuild()
    {
        Debug.Log("SetBuildigIsBuild");
        _buildList[0].GetComponent<Building>().ChangeSprite();
    }
    
    #endregion
}