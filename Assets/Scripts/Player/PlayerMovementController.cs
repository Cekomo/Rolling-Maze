using System.Collections;
using UnityEngine;
using Maze;

namespace Player
{
    public class PlayerMovementController : MonoBehaviour // increase of radius in each path 1.828, 3.655..
{
    private const int ARTIFICIAL_GRAVITY = 10;
    private const float TORQUE = 197; // make this accurate // was 300
    private const float ROLLING_TIME = 1f;
    private const float ROLLING_SPEED = 180f;

    private static Rigidbody _rbBall;

    private void Start()
    {
        _rbBall = GetComponent<Rigidbody>();
        _rbBall.maxAngularVelocity = 15.0f; // 20 
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
}