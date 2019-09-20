using System;
using UnityEngine;
using System.Collections;

[Serializable]
public class SaveData 
{

    [SerializeField]
    private int _lastLevelUnlocked = 0;
    public int LastLevelUnlocked
    {
        get { return _lastLevelUnlocked; }
        set { _lastLevelUnlocked = value; }
    }

    [SerializeField]
    private bool _isSoundOn = false;
    public bool IsSoundOn
    {
        get { return _isSoundOn; }
        set { _isSoundOn = value; }
    }

    [SerializeField]
    private Difficulty _difficulty = Difficulty.Normal;
    public Difficulty Difficulty
    {
        get { return _difficulty; }
        set { _difficulty = value; }
    }
}
