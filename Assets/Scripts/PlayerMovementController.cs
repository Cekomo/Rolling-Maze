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

        StartCoroutine(MoveBall());
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
                _rbBall.AddTorque(Vector3.back * TORQUE);
                break;
            case SwipeDirection.Left:
                _rbBall.AddTorque(Vector3.forward * TORQUE);
                break;
        }
    }

    private static void RevertTorque()
    {
        _rbBall.angularVelocity = Vector3.zero;
        _rbBall.velocity = Vector3.zero;
        print(_rbBall.transform.position.z);
    }
    
    private static IEnumerator MoveBall()
    {
        // var start = _rbBall.transform.position;
        AddTorque();
        MazeMovementController.PreventTurning = true;
        yield return new WaitForSeconds(ROLLING_TIME);
        RevertTorque();
        MazeMovementController.PreventTurning = false;
        // print(_rbBall.transform.position.z - start.z);
    }
}
