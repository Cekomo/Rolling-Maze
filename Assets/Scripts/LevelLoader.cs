using UnityEngine;

public static class LevelLoader
{
    private static int _levelIndex;

    public static void LoadLevel()
    {
        // some UI manipulations can be made here
    }

    public static int GetLevelWithOffset(int nextLevelIndex)
    {
        _levelIndex = PlayerPrefs.GetInt("LevelIndex");
        return _levelIndex + nextLevelIndex;
    }

    public static void SaveLevel()
    {
        PlayerPrefs.SetInt("LevelIndex", GetLevelWithOffset(1));
    }
}
