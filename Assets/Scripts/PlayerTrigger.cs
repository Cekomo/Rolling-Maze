    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class PlayerTrigger : MonoBehaviour
    {
        public MazeManager mazeManager;
        public UIManager uIManager;

        private void OnCollisionEnter(Collision col)
        {
            if (!col.gameObject.CompareTag("Wall")) return;
            
            print("Game Over! Level retried.");
            
            MazeMovementController.ResetRotationBehavior();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
            
            LevelLoader.PauseGame(true);
            uIManager.SetStartPanelStatus(true);
        }

        private void OnTriggerEnter(Collider col)
        {
            if (!col.gameObject.CompareTag("Finish")) return;
            
            LevelLoader.SaveLevel();
            
            // MazeMovementController.ResetRotationBehavior();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
            
            LevelLoader.PauseGame(true);
            uIManager.SetLevelCounter();
            uIManager.SetStartPanelStatus(true);
            
            mazeManager.InstantiateNewMaze();
            MazeManager.PrepareTheMaze();
        }
    }
