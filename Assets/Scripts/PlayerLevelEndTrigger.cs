using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLevelEndTrigger : MonoBehaviour
{
    public MazeManager mazeManager;
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
        
        LevelLoader.PauseGame(true);
        uIManager.SetStartPanelStatus(true);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (!col.gameObject.CompareTag("Finish")) return;
        
        LevelLoader.SaveLevel();
        
        SkinManager.GamePoint = PlayerPrefs.GetInt("GamePoint") + SkinManager.LevelPoint + 
                                PlayerPrefs.GetInt("PointMultiplier") * SkinManager.LevelPoint / 3;
        PlayerPrefs.SetInt("GamePoint", SkinManager.GamePoint);
        PlayerPrefs.SetInt("PointMultiplier", SkinManager.SCORE_MULTIPLIER);
        
        MazeMovementController.ResetRotationBehavior();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        
        LevelLoader.PauseGame(true);
        uIManager.SetLevelCounter();
        uIManager.SetStartPanelStatus(true);
        
        var theMazeScale = MazeModels.MazeScaleList[LevelLoader.GetLevel()];
        CameraMovementController.PausedOffset = new Vector3(0, theMazeScale * 5, theMazeScale * 4);

        mazeManager.InstantiateNewMaze();
        MazeManager.DecideMazeColor();
        
        print(PlayerPrefs.GetInt("GamePoint"));
    }

    private void OnCollisionExit(Collision col)
    {
        if (!col.gameObject.CompareTag("Floor")) return;

        PlayerTouchController.SwipeDirection = SwipeDirection.Lock;
    }
}
