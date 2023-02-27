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

        MazeMovementController.ResetRotationBehavior();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        
        LevelLoader.PauseGame(true);
        uIManager.SetStartPanelStatus(true);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (!col.gameObject.CompareTag("Finish")) return;
        
        LevelLoader.SaveLevel();
        
        MazeMovementController.ResetRotationBehavior();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        
        LevelLoader.PauseGame(true);
        uIManager.SetLevelCounter();
        uIManager.SetStartPanelStatus(true);
        
        var theMazeScale = MazeModels.MazeScaleList[LevelLoader.GetLevel()];
        CameraMovementController.PausedOffset = new Vector3(0, theMazeScale * 5, theMazeScale * 4);

        mazeManager.InstantiateNewMaze();
        // MazeManager.PrepareTheMaze();
    }

    private void OnCollisionExit(Collision col)
    {
        if (!col.gameObject.CompareTag("Floor")) return;

        PlayerTouchController.SwipeDirection = SwipeDirection.Lock;
    }
}
