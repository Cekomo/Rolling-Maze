using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLevelEndTrigger : MonoBehaviour
{
    public UIManager uIManager;
    public AudioManager audioManager;
    public AdManager adManager;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Floor")) 
            audioManager.Play(AudioType.BallFloorHit);
        
        if (!col.gameObject.CompareTag("Wall")) return;
        
        PlayerPrefs.SetInt("LevelTries", PlayerPrefs.GetInt("LevelTries") + 1);
        
        adManager.ShowAdInEvery3Attempt(); 

        if (AdManager.AdShowingCounter != 0)
        {
            MazeMovementController.ResetRotationBehavior();                                    
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);  
        }
        
        LevelLoader.PauseGame(true);
        uIManager.SetStartPanelStatus(true);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (!col.gameObject.CompareTag("Finish")) return;
        GameManager.IsLevelCompleted = true;
        
        uIManager.SetLevelTriesText();
        GameManager.SetLevelGoldGain();
        uIManager.SetLevelGainText(uIManager.goldGainText, 1);
        GameManager.IncreaseTotalGoldByFactor(1);
        
        LevelLoader.SaveLevel();
        
        PlayerPrefs.SetInt("LevelTries", 0);

        MazeMovementController.ResetRotationBehavior();
        LevelLoader.PauseGame(true);
        
        adManager.ShowAdInEvery3Attempt();
        
        uIManager.SetLevelEndPanel();
    }

    private void OnCollisionExit(Collision col)
    {
        if (!col.gameObject.CompareTag("Floor")) return;
        PlayerTouchController.SwipeDirection = SwipeDirection.Lock;
    }
}
