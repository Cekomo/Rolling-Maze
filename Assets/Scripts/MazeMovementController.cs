using System;
using UnityEngine;

public class MazeMovementController : MonoBehaviour
{   // coupling in playerMovementController class, for booleans, handle if you can
    private const float INITIAL_ANGULAR_SPEED = 100f;
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

    public static void AdjustAngularSpeed() 
    { // it can be good to decrease angular speed decrement of the maze to polish gameplay
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

    public static void ResetRotationBehavior()
    {
        PreventRotation = true;
        _rotationDirection = 1;
        _currentPathRadius = RADIUS_RING_DIFFERENCE;
        _angularSpeed = INITIAL_ANGULAR_SPEED;
    }
}
