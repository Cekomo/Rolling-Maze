using UnityEngine;

public class CameraMovementController : MonoBehaviour
{
    [SerializeField] private Transform target;
    private static readonly Vector3 OFFSET = new(0f, 10f, -10f);

    private void LateUpdate()
    {
        transform.position = target.position + OFFSET;
    }
}
