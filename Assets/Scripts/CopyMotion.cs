using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyMotion : MonoBehaviour
{
    public Transform targetLimb;
    ConfigurableJoint cj;
    Quaternion initialRotationDifference;

    // Start is called before the first frame update
    void Start()
    {
        cj = GetComponent<ConfigurableJoint>();
        if (cj.connectedBody != null)
        {
            // Calculate the initial rotation difference between the targetLimb and the connected body.
            initialRotationDifference = Quaternion.Inverse(targetLimb.localRotation) * cj.connectedBody.transform.localRotation;
        }
        else
        {
            Debug.LogError("Joint's connected body is null.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the joint's connected body (connectedBody) is not null.
        if (cj.connectedBody != null)
        {
            // Calculate the current rotation difference between the targetLimb and the connected body.
            Quaternion currentRotationDifference = Quaternion.Inverse(targetLimb.localRotation) * cj.connectedBody.transform.localRotation;

            // Calculate the relative rotation from the initialRotationDifference.
            Quaternion relativeRotation = currentRotationDifference * Quaternion.Inverse(initialRotationDifference);

            // Set the local target rotation of the joint.
            cj.targetRotation = relativeRotation;
        }
        else
        {
            Debug.LogError("Joint's connected body is null.");
        }
    }
}
