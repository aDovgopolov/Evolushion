using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyUI : MonoBehaviour
{
    #region fields
    
    public static CurrencyUI Instance { get; protected set; }
    
    [Header("UI")]
    public Text armyText;
    public Text foodText;
    public Text coinText;
    public Text gemsText;
    
    #endregion
    
    private void Awake()
    {
        Instance = this;
    }
    

    public void OpenSettings()
    {
        // TODO settings canvas menu
    }
    
}
