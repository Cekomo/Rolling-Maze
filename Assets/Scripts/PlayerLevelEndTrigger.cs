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

        if (PlayerPrefs.GetInt("PointMultiplier") > 0)
            PlayerPrefs.SetInt("PointMultiplier", PlayerPrefs.GetInt("PointMultiplier") - 1);
        
        MazeMovementController.ResetRotationBehavior();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);

        AdManager.ShowAdAfterLevelCompletion(); // maybe after start-panel initialization?
     
        LevelLoader.PauseGame(true);
        uIManager.SetStartPanelStatus(true);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (!col.gameObject.CompareTag("Finish")) return;
        
        LevelLoader.SaveLevel();
        
        SkinManager.GamePoint = PlayerPrefs.GetInt("GamePoint") + 
                                SkinManager.LevelPoint * (1 + PlayerPrefs.GetInt("PointMultiplier") / 3);
        PlayerPrefs.SetInt("GamePoint", SkinManager.GamePoint);
        PlayerPrefs.SetInt("PointMultiplier", SkinManager.SCORE_MULTIPLIER);

        MazeMovementController.ResetRotationBehavior();
        LevelLoader.PauseGame(true);

        AdManager.ShowAdAfterLevelCompletion();
        uIManager.SetLevelEndPanel();
    }

    private void OnCollisionExit(Collision col)
    {
        if (!col.gameObject.CompareTag("Floor")) return;

        PlayerTouchController.SwipeDirection = SwipeDirection.Lock;
    }
    
    
}
