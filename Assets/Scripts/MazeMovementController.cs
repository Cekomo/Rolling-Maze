using UnityEngine;

public class MazeMovementController : MonoBehaviour
{   // coupling in playerMovementController class, for booleans, handle if you can
    private const float RADIUS_RING_DIFFERENCE = 1.825f;
    private static float _angularSpeed = 100f;
    private static float _currentPathRadius = 1.825f;

    private static int _rotationDirection = 1;
    public static bool PreventRotation = true;

    private void Update()
    {
        if (PreventRotation) return;
        
        transform.Rotate(0f, GetRotationDirection() * _angularSpeed * Time.deltaTime, 0f);
    }

    public static void AdjustAngularSpeed() // angular speed per ring seems okay
    {
        _angularSpeed = _angularSpeed * _currentPathRadius / (_currentPathRadius + RADIUS_RING_DIFFERENCE);
        _currentPathRadius += RADIUS_RING_DIFFERENCE;
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