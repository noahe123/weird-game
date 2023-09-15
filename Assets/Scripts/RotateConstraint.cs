using UnityEngine;

public class RotateConstraint : MonoBehaviour
{
    public bool constrainXRotation = false; // Set this to true to constrain X-axis rotation
    public bool constrainYRotation = false; // Set this to true to constrain Y-axis rotation
    public bool constrainZRotation = false; // Set this to true to constrain Z-axis rotation

    private Vector3 initialRotation;

    private void Start()
    {
        // Store the object's initial rotation when the script starts
        initialRotation = transform.rotation.eulerAngles;
    }

    private void Update()
    {
        // Get the current rotation of the object
        Vector3 currentRotation = transform.rotation.eulerAngles;

        // Apply constraints based on the specified axes
        if (constrainXRotation)
        {
            currentRotation.x = initialRotation.x;
        }

        if (constrainYRotation)
        {
            currentRotation.y = initialRotation.y;
        }

        if (constrainZRotation)
        {
            currentRotation.z = initialRotation.z;
        }

        // Set the object's rotation with the constraints applied
        transform.rotation = Quaternion.Euler(currentRotation);
    }
}
