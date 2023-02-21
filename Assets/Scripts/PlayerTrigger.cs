    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class PlayerTrigger : MonoBehaviour
    {
        public MazeManager mazeManager;

        private void OnCollisionEnter(Collision col)
        {
            if (!col.gameObject.CompareTag("Wall")) return;
            
            print("Game Over! Level retried.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void OnTriggerExit(Collider col)
        {
            if (!col.gameObject.CompareTag("Finish")) return;
            
            LevelLoader.SaveLevel();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            
            mazeManager.InstantiateNewMaze();
            MazeManager.PrepareTheMaze();
        }
    }
