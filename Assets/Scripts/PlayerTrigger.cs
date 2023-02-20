using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTrigger : MonoBehaviour
{
    private void OnCollisionEnter(Collision col)
    {
        if (!col.gameObject.CompareTag("Wall")) return;
        
        print("Game Over! Level retried.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnCollisionExit(Collision col)
    {
        if (!col.gameObject.CompareTag("Floor") || transform.position.z < 5)
            return;

        print("You Win! Next level initiated.");

    }
}
