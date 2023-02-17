using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchController : MonoBehaviour
{
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;
    
    private const int MIN_SWIPE_DISTANCE = 20;

    private SwipeDirection swipeDirection;

    public SwipeDirection SwipeDirection
    {
        set => swipeDirection = value;
    }
    
    private void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    fingerUpPosition = touch.position;
                    fingerDownPosition = touch.position;
                    swipeDirection = SwipeDirection.None;
                    break;
                case TouchPhase.Moved:
                    fingerDownPosition = touch.position;
                    DetectSwipe();
                    break;
                case TouchPhase.Ended:
                    fingerDownPosition = touch.position;
                    DetectSwipe();
                    break;
            }
        }
    }

    private void DetectSwipe()
    {
        if (!SwipeDistanceCheck()) return;
        
        swipeDirection = GetSwipeDirection();
        if (swipeDirection != SwipeDirection.None)
        {
            Debug.Log("Swipe Direction: " + swipeDirection);
        }

        fingerDownPosition = fingerUpPosition;
    }

    private bool SwipeDistanceCheck()
    {
        return Vector3.Distance(fingerDownPosition, fingerUpPosition) > MIN_SWIPE_DISTANCE;
    }

    private SwipeDirection GetSwipeDirection()
    {
        Vector2 swipeVector = fingerDownPosition - fingerUpPosition;

        // if (swipeVector.magnitude < MIN_SWIPE_DISTANCE)
        // {
        //     return SwipeDirection.None;
        // }

        var angle = Vector2.Angle(swipeVector, Vector2.up);

        switch (Mathf.RoundToInt(angle / 45f))
        {
            case 0:
            case 8:
                return SwipeDirection.Up;
            case 1:
            case 2:
            case 3:
                return SwipeDirection.Right;
            case 5:
            case 6:
            case 7:
                return SwipeDirection.Left;
            default:
                return SwipeDirection.None;
        }
    }
}
