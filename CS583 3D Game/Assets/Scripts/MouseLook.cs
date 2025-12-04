using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    private float mouseSensitivity = 1000f;

    [SerializeField]
    private Transform playerBody; //reference to parent body

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //locks cursor to center of screen + hides it
        Debug.Log("Use ESC to exit locked cursor mode.");
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //restricts camera movement to avoid flipping

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); //rotates camera vertically
        playerBody.Rotate(Vector3.up * mouseX); //rotates camera horizontally
    }
}
