using UnityEngine;

public static class LevelLoader
{
    public static bool IsPaused;

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
        
        PlayerPrefs.SetInt("LevelIndex", GetLevel() + 5);
    }

    public static void PauseGame(bool isPaused)
    {
        if (isPaused)
        {
            Time.timeScale = 0;
            PlayerTouchController.SwipeDirection = SwipeDirection.Lock;
        }
        else
            Time.timeScale = 1;
      
        IsPaused = isPaused;
    }
    
    
}
