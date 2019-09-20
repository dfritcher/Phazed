using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _levelDisplay = null;

    private static  LevelDisplay _instance = null;
    public static LevelDisplay Instance
    {
        get { return _instance; }
    }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void SetLevelDisplay(int levelNumber)
    {
        _levelDisplay.text = $"LEVEL - {levelNumber}";
    }
}
