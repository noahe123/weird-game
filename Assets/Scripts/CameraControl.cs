using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float rotationSpeed = 1f;
    public Transform root;
    public float smoothTime = 0.2f; // Smoothing time for camera movement
    public float rotationSmoothTime = 0.1f; // Smoothing time for rotation

    private float mouseX, mouseY;
    private float smoothX, smoothY, smoothXVelocity, smoothYVelocity;

    public float headOffset;
    public ConfigurableJoint hipJoint, headJoint;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
      //  CamControl();
    }

    void CamControl()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed; // Inverted Y-axis
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        // Smoothly interpolate the camera's position
        smoothX = Mathf.SmoothDamp(smoothX, mouseX, ref smoothXVelocity, smoothTime);
        smoothY = Mathf.SmoothDamp(smoothY, mouseY, ref smoothYVelocity, smoothTime);

        //Quaternion rootRotation = Quaternion.Euler(smoothY, smoothX, 0);



        //hipJoint.targetRotation = Quaternion.Euler(0, -smoothX, 0);
        //headJoint.targetRotation = Quaternion.Euler(-smoothY + headOffset, 0, 0);
    }
}
