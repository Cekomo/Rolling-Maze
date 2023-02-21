using UnityEngine;

public static class LevelLoader
{
    public static void LoadLevel()
    {
        // some UI manipulations can be made here
    }

    public static int GetLevel()
    {
        return PlayerPrefs.GetInt("LevelIndex");
    }

    public static void SaveLevel()
    {
        PlayerPrefs.SetInt("LevelIndex", GetLevel() + 1);
    }
}
