using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTrigger : MonoBehaviour
{
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Wall"))
        {
            print("Game Over! Level retried.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
