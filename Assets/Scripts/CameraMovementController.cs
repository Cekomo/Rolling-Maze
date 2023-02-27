using UnityEngine;

public class CameraMovementController : MonoBehaviour
{
    [SerializeField] private Transform target;
    
    public static Vector3 PausedOffset;
    private static readonly Vector3 FollowingOffset = new(0f, 10f, -10f);

    private Vector3 _velocity = Vector3.zero;

    private void LateUpdate()
    {
        var targetPosition = target.position;
        var currentOffset = LevelLoader.IsPaused ? PausedOffset : FollowingOffset;
        
        if (PlayerTouchController.SwipeDirection is SwipeDirection.Lock && !LevelLoader.IsPaused) 
            currentOffset = Vector3.SmoothDamp(transform.position - targetPosition, currentOffset, 
                ref _velocity, 0.1f, Mathf.Infinity, Time.deltaTime);

        transform.position = targetPosition + currentOffset;
    }
    
    // lerp function can cause slight performance improvement
    // private const int INITIAL_CAMERA_SPEED = 10;
    // if (PlayerTouchController.SwipeDirection is SwipeDirection.Lock && !LevelLoader.IsPaused) 
    //     currentOffset = Vector3.Lerp(transform.position - targetPosition, currentOffset,
    //      Time.deltaTime * INITIAL_CAMERA_SPEED);
}