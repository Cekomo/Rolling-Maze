using UnityEngine;

public class MazeMovementController : MonoBehaviour
{   // coupling in playerMovementController class, for booleans, handle if you can
    private static float angularSpeed = 60f;
    private static float currentPathRadius = 1.825f;

    public static bool PreventRotation = true;
    private static int _rotationDirection = 1;
    
    private void Update()
    {
        if (PreventRotation) return;
    
        transform.Rotate(0f, GetRotationDirection() * angularSpeed * Time.deltaTime, 0f);
    }

    public static void AdjustAngularSpeed()
    {
        angularSpeed = angularSpeed * currentPathRadius / (currentPathRadius + Mathf.Sqrt(1.825f));
    }
    
    public static void RotateTowards(int rotationMultiplier)
    {
        _rotationDirection = rotationMultiplier;
    }
    
    public static int GetRotationDirection()
    {
        return _rotationDirection;
    }
}
