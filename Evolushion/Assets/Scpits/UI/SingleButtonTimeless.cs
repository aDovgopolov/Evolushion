﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingleButtonTimeless : MonoBehaviour
{
    public void LoadFarmScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;
//        Debug.Log($"{sceneName}");
        if (sceneName.Equals("Ferm"))
        {
            SceneController.Instance.TransitionToScene();
        }
        else
        {
            GameManager.Instance.gameData.GetBuildings()["ferma"].IsBuilt = true;
            
            BuildingManager.instance.SetBuildigIsBuild();
            //BuildingManager.instance.SetBuildigIsBuild();
        }
    }
}
