using System;
using UnityEngine;

public class CameraMovementController : MonoBehaviour
{
    [SerializeField] private Transform target;
    
    // below component must be dynamic wrt maze radius
    private static readonly Vector3 PausedOffset = new(0, 35f, -25f);
    private static readonly Vector3 FollowingOffset = new(0f, 10f, -10f);

    private void LateUpdate()
    {
        var currentOffset = LevelLoader.IsPaused ? PausedOffset : FollowingOffset;
        transform.position = target.position + currentOffset;
    }
}
