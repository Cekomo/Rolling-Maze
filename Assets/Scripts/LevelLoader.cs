using UnityEngine;

public static class LevelLoader
{
    public static bool IsPaused;
    
    
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
        if (GetLevel() == MazeModels.MaximumLevel)
        {
            Debug.Log("Congrats! You completed all levels.");
            return;
        }
        
        PlayerPrefs.SetInt("LevelIndex", GetLevel() + 1);
    }

    public static void PauseGame(bool isPaused)
    {
        Time.timeScale = isPaused ? 0 : 1;
        IsPaused = isPaused;
    }
}
