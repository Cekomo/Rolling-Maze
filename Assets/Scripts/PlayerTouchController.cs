using UnityEngine;

// in this form, finger must be up after swipe operation for ball to move

public class PlayerTouchController : MonoBehaviour
{
    private Vector2 _fingerDownPosition;
    private Vector2 _fingerUpPosition;
    
    
    
    // handle the constant below, pixel ratio varies from phone to phone
    private const int MIN_SWIPE_DISTANCE = 30;

    public static SwipeDirection SwipeDirection { get; set; }

    private void Update()
    {
        if (Input.touchCount == 0) return;
        
        foreach (var touch in Input.touches)
        {
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _fingerDownPosition = touch.position;
                    SwipeDirection = SwipeDirection.None;
                    break;
                case TouchPhase.Ended:
                    _fingerUpPosition = touch.position;
                    DetectSwipe();
                    break;
            }
        }
    }

    private void DetectSwipe()
    {
        if (!SwipeDistanceCheck()) return;
        
        SwipeDirection = GetSwipeDirection();
    }

    private bool SwipeDistanceCheck()
    {
        return Vector3.Distance(_fingerDownPosition, _fingerUpPosition) > MIN_SWIPE_DISTANCE;
    }

    private SwipeDirection GetSwipeDirection()
    {
        var swipeVector = _fingerDownPosition - _fingerUpPosition;

        var angle = Vector2.Angle(swipeVector, Vector2.up); // angle is 0 to 180    
        
        switch (Mathf.CeilToInt(angle / 45f))
        {
            case 4:
                return SwipeDirection.Up;
            case 2: case 3:
                return swipeVector.x > 0 ? SwipeDirection.Left : SwipeDirection.Right;
            default:
                return SwipeDirection.None;
        }
    }
}
