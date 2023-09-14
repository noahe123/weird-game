using UnityEngine;

public class YAxisBillboard : MonoBehaviour
{
    public Vector3 rotationalOffset = Vector3.zero;

    private Transform mainCameraTransform;

    private void Start()
    {
        // Find the main camera in the scene
        mainCameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        // Calculate the direction from the object to the camera
        Vector3 lookDirection = mainCameraTransform.position - transform.position;

        // Create a rotation based on the direction and apply the rotational offset
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection) * Quaternion.Euler(rotationalOffset);

        // Apply the rotation to the object
        transform.rotation = targetRotation;
    }
}
