using System;
using UnityEngine;

public class CameraMovementController : MonoBehaviour
{
    [SerializeField] private Transform target;
    
    // below component must be dynamic wrt maze radius
    public static Vector3 PausedOffset;
    private static readonly Vector3 FollowingOffset = new(0f, 10f, -10f);

    private void LateUpdate()
    {
        var currentOffset = LevelLoader.IsPaused ? PausedOffset : FollowingOffset;
        transform.position = target.position + currentOffset;
    }
}

/*
 * if (!LevelLoader.IsPaused || Input.touchCount == 0) return;
        var targetPosition = target.position + FollowingOffset;
        var cameraPosition = transform.position;
        var speed = 10f;

        while (Vector3.Distance(cameraPosition, targetPosition) > 0.01f)
        {
            cameraPosition = Vector3.MoveTowards(cameraPosition, targetPosition, speed * Time.deltaTime);
            transform.position = cameraPosition;
        }
*/