using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float mouseSensitivity = 150f;
    public Transform playerBody;    // your knight
    public Transform cameraHolder;  // the empty object holding the camera

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate the PLAYER left/right
        playerBody.Rotate(Vector3.up * mouseX);

        // Rotate CAMERA up/down (but clamp it)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cameraHolder.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
