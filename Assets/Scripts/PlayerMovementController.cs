using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private const float TORQUE = 20;

    private static Rigidbody _rbBall;

    private void Start()
    {
        _rbBall = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (PlayerTouchController.SwipeDirection == SwipeDirection.None) return;
        
        MoveBall();
        PlayerTouchController.SwipeDirection = SwipeDirection.None;
    }

    private static void MoveBall()
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
}
