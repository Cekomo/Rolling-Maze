using System;
using System.Collections;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour // increase of radius in each path 1.828, 3.655..
{
    private const float TORQUE = 195;
    private const float ROLLING_TIME = 1f;

    private static Rigidbody _rbBall;

    private void Start()
    {
        _rbBall = GetComponent<Rigidbody>();
        _rbBall.maxAngularVelocity = 20.0f;
    }

    private void Update()
    {
        if (MazeMovementController.PreventRotation) return;
        
        RotateInPlace();
    }

    private void FixedUpdate()
    {
        if (PlayerTouchController.SwipeDirection == SwipeDirection.None) return;

        switch (PlayerTouchController.SwipeDirection)
        {
            case SwipeDirection.Up:
                StartCoroutine(MoveBall());
                break;
            case SwipeDirection.Right:
                // appears to roll the ball towards right
                MazeMovementController.RotateTowards(-1);
                break;
            case SwipeDirection.Left:
                // appears to roll the ball towards left
                MazeMovementController.RotateTowards(1);
                break;
        }
        
        PlayerTouchController.SwipeDirection = SwipeDirection.None;
    }

    private static void AddTorque() // mass = 3
    {
        if (PlayerTouchController.SwipeDirection == SwipeDirection.None) return;
        _rbBall.AddTorque(Vector3.right * TORQUE);
    }

    private static void RevertTorque()
    {
        _rbBall.angularVelocity = Vector3.zero;
        _rbBall.velocity = Vector3.zero;
    }
    
    private static IEnumerator MoveBall()
    {
        AddTorque();
        MazeMovementController.PreventRotation = true;
        yield return new WaitForSeconds(ROLLING_TIME);
        
        RevertTorque();
        MazeMovementController.PreventRotation = false;
    }

    private void RotateInPlace()
    {
        // transform.Rotate(Vector3.up, 45f * Time.deltaTime, Space.Self);
        // transform.Rotate(0, 0, 45f * Time.deltaTime * -MazeMovementController.RotationDirection);
        // transform.rotation *= 
        //     Quaternion.Euler(0, 0, 45f * Time.deltaTime * -MazeMovementController.RotationDirection);
        transform.rotation *= 
            Quaternion.Euler(0f, 0f, 45f * Time.deltaTime * -MazeMovementController.RotationDirection());
    }
}

/*
 * private static void AddTorque() // mass = 3
    {
        if (PlayerTouchController.SwipeDirection == SwipeDirection.None) return;
    
        // do not confuse with vector3 directions, it is torque implementation
        switch (PlayerTouchController.SwipeDirection)
        {
            case SwipeDirection.Up:
                _rbBall.AddTorque(Vector3.right * TORQUE);
                break;
            case SwipeDirection.Right:
                print("a");
                MazeMovementController.RotationDirection = -1;
                // _rbBall.AddTorque(Vector3.back * TORQUE);
                break;
            case SwipeDirection.Left:
                // _rbBall.AddTorque(Vector3.forward * TORQUE);
                break;
        }
    }
*/