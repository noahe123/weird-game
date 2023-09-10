using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        if (mainCamera != null)
        {
            // Calculate the direction from the GameObject to the camera
            Vector3 directionToCamera = mainCamera.transform.position - transform.position;

            // Rotate the object to face the camera
            transform.rotation = Quaternion.LookRotation(directionToCamera);
        }
    }
}
