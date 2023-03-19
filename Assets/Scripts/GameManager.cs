using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static int _levelGoldGain;
    
    public static bool IsStoreActive { get; set; }
    public static bool IsEndPanelActive { get; set; }

    public static void SetLevelGoldGain()
    {
        var levelTriesMultiplier = Mathf.Max(3 - PlayerPrefs.GetInt("LevelTries"), 0);
        var levelGainMultiplier = 1 + levelTriesMultiplier / 2f;
        
        _levelGoldGain = (int)(SkinManager.LevelPoint * levelGainMultiplier);
    }

    public static int GetLevelGoldGain()
    {
        return _levelGoldGain;
    }

    public static void SetTotalGold()
    {
        var updatedTotalGold = PlayerPrefs.GetInt("GamePoint") + GetLevelGoldGain();
        PlayerPrefs.SetInt("GamePoint", updatedTotalGold);
    }
    // start mechanism in UIManager will be implemented here
}
