using System.Collections;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour // increase of radius in each path 1.828, 3.655..
{
    private const int ARTIFICIAL_GRAVITY = 15;
    private const int ROLLING_SPEED = 200;
    private const float RING_DISTANCE = 2f;
    private const float ROLLING_TIME = 0.3f;
    private const float VELOCITY = RING_DISTANCE / ROLLING_TIME;

    private static Rigidbody _rbBall;

    private void Start()
    {
        _rbBall = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (MazeMovementController.PreventRotation) return;
        
        RotateInCircularPath();
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

    private IEnumerator MoveBall()
    {
        MazeMovementController.PreventRotation = true;
        
        var startPosition = transform.position;
        var endPosition = new Vector3(startPosition.x, startPosition.y, startPosition.z + RING_DISTANCE);

        while (Vector3.Distance(transform.position, endPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, 
                VELOCITY * Time.deltaTime);
            RotateForward();
            yield return null;
        }
  
        if (PlayerTouchController.SwipeDirection == SwipeDirection.Lock) yield break;
        MazeMovementController.PreventRotation = false;
    }

    private void RotateForward()
    {
        transform.Rotate(ROLLING_SPEED * Time.deltaTime * 3, 0f, 0f, Space.World);
    }

    private void RotateInCircularPath()
    {
        transform.Rotate(0f, 0f, 
            MazeMovementController.GetRotationDirection() * ROLLING_SPEED * Time.deltaTime, 
            Space.World);
    }
}