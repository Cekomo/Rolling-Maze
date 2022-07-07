using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float moveSpeed = 0.5f;
    private float scrollSpeed = 10f;

    private bool a = false;
    private float cameraSpeedX = 15f; // adjusted to control speef of y-axis of camera
    
    // Note that ideal camera position will change with respect to maze size
    // X: 65 degree may be good for remote look 
    // X: 0, Y: 17 Z: -6 | Remote look

    // Below adjustments are used for a long time..
    // Standard rotation X: 52 degree
    // X: 0, Y: 7.25f Z: -1.61f | Standard look

    // TRY TO MAKE THE CAMERA MOVING BETWEEN TWO LOCATIONS

    private Vector3 firstView = new Vector3(0f, 7f, -2.3f);

    void Start()
    {
        transform.position = new Vector3(0f, 17f, -6f);
        transform.rotation = Quaternion.Euler(60, 0, 0); // sets camera angle to 52
    }

    void Update()
    {
        // Below statements are for controlling the camera with WASD (arrow keys) and scroll wheel 
        // -----------------------------------
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            transform.position += moveSpeed * new Vector3(Input.GetAxisRaw("Horizontal") / 100, 0, Input.GetAxisRaw("Vertical") / 100);

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
            transform.position += scrollSpeed * new Vector3(0, -Input.GetAxis("Mouse ScrollWheel"), 0);
        // -----------------------------------

        if (Input.GetMouseButtonDown(0))
            a = true;

        if (a && transform.position.y > 7)
            transform.Translate(cameraSpeedX * -Vector3.up * Time.deltaTime, Space.World);
        if (a && transform.position.z < -2.3)
            transform.Translate(cameraSpeedX * 0.37f * Vector3.forward * Time.deltaTime, Space.World);
    }

}