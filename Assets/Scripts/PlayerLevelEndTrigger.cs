using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLevelEndTrigger : MonoBehaviour
{
    public UIManager uIManager;
    public AudioManager audioManager;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Floor")) 
            audioManager.Play(AudioType.BallFloorHit);
        
        if (!col.gameObject.CompareTag("Wall")) return;
        
        PlayerPrefs.SetInt("LevelTries", PlayerPrefs.GetInt("LevelTries") + 1); // check if prefs get updated correctly
        MazeMovementController.ResetRotationBehavior();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);

        // AdManager.ShowAdAfterLevelCompletion(); // maybe after start-panel initialization?
     
        LevelLoader.PauseGame(true);
        uIManager.SetStartPanelStatus(true);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (!col.gameObject.CompareTag("Finish")) return;
        
        uIManager.SetLevelTriesText();
        GameManager.SetLevelGoldGain();
        uIManager.SetLevelGainText();
        GameManager.SetTotalGold();
        
        LevelLoader.SaveLevel();
        
        PlayerPrefs.SetInt("LevelTries", 0);
        MazeMovementController.ResetRotationBehavior();
        
        LevelLoader.PauseGame(true);
        
        // AdManager.ShowAdAfterLevelCompletion();
        uIManager.SetLevelEndPanel();
    }

    private void OnCollisionExit(Collision col)
    {
        if (!col.gameObject.CompareTag("Floor")) return;

        PlayerTouchController.SwipeDirection = SwipeDirection.Lock;
    }
    
    
}
