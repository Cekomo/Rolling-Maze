using System.Collections;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour // increase of radius in each path 1.828, 3.655..
{
    private const int ARTIFICIAL_GRAVITY = 15;
    private const float TORQUE = 638; 
    private const float ROLLING_TIME = 0.3f;
    private const float ROLLING_SPEED = 300f;

    private static Rigidbody _rbBall;

    private void Start()
    {
        _rbBall = GetComponent<Rigidbody>();
        _rbBall.maxAngularVelocity = 60f; 
    }

    private void Update()
    {
        if (MazeMovementController.PreventRotation) return;
        
        RotateInPlace();
    }

    private void FixedUpdate()
    {
        if (PlayerTouchController.SwipeDirection is SwipeDirection.Lock && !LevelLoader.IsPaused) 
            _rbBall.velocity = Vector3.down * ARTIFICIAL_GRAVITY;
        
        if (PlayerTouchController.SwipeDirection is SwipeDirection.None or SwipeDirection.Lock) return;

        switch (PlayerTouchController.SwipeDirection)
        {
            case SwipeDirection.Up:
                StartCoroutine(MoveBall());
                MazeMovementController.AdjustAngularSpeed();
                break;
            case SwipeDirection.Right:
                MazeMovementController.RotateTowards(-1);
                break;
            case SwipeDirection.Left:
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

        if (PlayerTouchController.SwipeDirection == SwipeDirection.Lock) yield break;
        RevertTorque();
        MazeMovementController.PreventRotation = false;
    }

    private void RotateInPlace()
    {
        transform.Rotate(0f, 0f, 
            MazeMovementController.GetRotationDirection() * ROLLING_SPEED * Time.deltaTime, 
            Space.World);
    }
}