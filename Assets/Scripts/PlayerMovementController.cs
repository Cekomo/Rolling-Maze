using System.Collections;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour // 1.828, 3.655..
{
    private const float TORQUE = 100;
    private const float ROLLING_TIME = 1f;

    private static Rigidbody _rbBall;

    private void Start()
    {
        _rbBall = GetComponent<Rigidbody>();
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
                MazeMovementController.RotationDirection = -1;
                break;
            case SwipeDirection.Left:
                // appears to roll the ball towards left
                MazeMovementController.RotationDirection = 1;
                break;
        }
        
        PlayerTouchController.SwipeDirection = SwipeDirection.None;
    }

    private static void AddTorque() // mass = 3
    {
        if (PlayerTouchController.SwipeDirection == SwipeDirection.None) return;
    
        // do not confuse with vector3 directions, it is torque implementation
        switch (PlayerTouchController.SwipeDirection)
        {
            case SwipeDirection.Up:
                _rbBall.AddTorque(Vector3.right * TORQUE);
                break;
            case SwipeDirection.Right:
                MazeMovementController.RotationDirection = -1;
                // _rbBall.AddTorque(Vector3.back * TORQUE);
                break;
            case SwipeDirection.Left:
                MazeMovementController.RotationDirection = 1;
                // _rbBall.AddTorque(Vector3.forward * TORQUE);
                break;
        }
    }

    private static void RevertTorque()
    {
        _rbBall.angularVelocity = Vector3.zero;
        _rbBall.velocity = Vector3.zero;
    }
    
    private static IEnumerator MoveBall()
    {
        // var start = _rbBall.transform.position;
        AddTorque();
        MazeMovementController.PreventRotation = true;
        yield return new WaitForSeconds(ROLLING_TIME);
        RevertTorque();
        MazeMovementController.PreventRotation = false;
        // print(_rbBall.transform.position.z - start.z);
    }
}
