using UnityEngine;

public class MazeMovementController : MonoBehaviour
{   // coupling in playerMovementController class, for booleans, handle if you can
    [SerializeField] private float rotationSpeed = 10f;

    public static bool PreventRotation = true;
    private static int _rotationDirection = 1;
    
    private void Update()
    {
        if (PreventRotation) return;
    
        transform.Rotate(0f, RotationDirection() * rotationSpeed * Time.deltaTime, 0f);
    }

    public static void RotateTowards(int rotationMultiplier)
    {
        _rotationDirection = rotationMultiplier;
    }
    
    public static int RotationDirection()
    {
        return _rotationDirection;
    }
}
