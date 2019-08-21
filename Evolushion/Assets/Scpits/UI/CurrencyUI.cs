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
    [Space]
    public Text settingsText;
    
    #endregion
    
    private void Awake()
    {
        Instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OpenSettings()
    {
        // TODO settings canvas menu
    }
    
}
