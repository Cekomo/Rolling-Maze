using UnityEngine;

public class MazeMovementController : MonoBehaviour
{   // coupling in playerMovementController class, for booleans, handle if you can
    public float RotationSpeed = 10f;

    public static bool PreventRotation = true;
    public static int RotationDirection = 1;
    
    private void Update()
    {
        if (PreventRotation) return;
        
        transform.Rotate(0f, RotationDirection * RotationSpeed * Time.deltaTime, 0f);
    }
}
