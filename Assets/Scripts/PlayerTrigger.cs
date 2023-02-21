    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class PlayerTrigger : MonoBehaviour
    {
        public MazeManager mazeManager;

        private void OnCollisionEnter(Collision col)
        {
            if (!col.gameObject.CompareTag("Wall")) return;
            
            print("Game Over! Level retried.");
            MazeMovementController.ResetRotationBehavior();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }

        private void OnTriggerEnter(Collider col)
        {
            if (!col.gameObject.CompareTag("Finish")) return;
            
            LevelLoader.SaveLevel();
            
            MazeMovementController.ResetRotationBehavior();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
            
            mazeManager.InstantiateNewMaze();
            MazeManager.PrepareTheMaze();
        }
    }
