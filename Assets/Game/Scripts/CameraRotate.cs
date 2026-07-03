using UnityEngine;
using Unity.Netcode;

public class CameraRotate : NetworkBehaviour
{
    public Transform cameraPivot;
    public float sensitivity = 200f;
    public float minRotationAngle = -60f;
    public float maxRotationAngle = 60f;

    private float _xRotation = 0f;

    void Start()
    {
        if (!IsOwner)
        {
            return;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void Update()
    {
        if (!IsOwner) { return; }

        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, minRotationAngle, maxRotationAngle);

        cameraPivot.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }
}
