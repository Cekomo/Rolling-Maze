using UnityEngine;

public class MazeMovementController : MonoBehaviour
{
    public float RotationSpeed = 10f;

    public static bool PreventTurning = true;
    
    private void Update()
    {
        if (PreventTurning) return;
        
        transform.Rotate(0f, RotationSpeed * Time.deltaTime, 0f);
    }
}
