using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool IsStoreActive { get; set; }
    public static bool IsEndPanelActive { get; set; }

    public static int LevelTries { get; set; } // convert this into array to store all level tries via gameplay

    private void Awake()
    {
        LevelTries = PlayerPrefs.GetInt("LevelTries");
        if (LevelTries == 0) LevelTries = 1;
    }

    // start mechanism in UIManager will be implemented here
}
