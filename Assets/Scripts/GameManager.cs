using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static int _levelGoldGain;
    public static int LevelTriesMultiplier;
    public static bool IsLevelCompleted;
    
    public static bool IsStoreActive { get; set; }
    public static bool IsEndPanelActive { get; set; }
    public static bool IsAdsActive { get; set; }

    // private void Start()
    // {
    //     IsLevelCompleted = false;
    // }

    public static void SetLevelGoldGain()
    {
        LevelTriesMultiplier = Mathf.Max(3 - PlayerPrefs.GetInt("LevelTries"), 0);
        var levelGainMultiplier = 1 + LevelTriesMultiplier / 2f;
        
        _levelGoldGain = (int)(SkinManager.LevelPoint * levelGainMultiplier);
    }

    public static int GetLevelGoldGain()
    {
        return _levelGoldGain;
    }

    public static void IncreaseTotalGoldByFactor(int multiplier)
    {
        var updatedTotalGold = PlayerPrefs.GetInt("GamePoint") + GetLevelGoldGain() * multiplier;
        PlayerPrefs.SetInt("GamePoint", updatedTotalGold);
    }
    // start mechanism in UIManager will be implemented here
}
