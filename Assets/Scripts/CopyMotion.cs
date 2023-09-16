using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CopyMotion : MonoBehaviour
{


    Transform otherSkeletonRoot; // Assign the root GameObject of the other skeleton in the Inspector.

    Transform matchingLimb;


    public Transform targetLimb;
    ConfigurableJoint cj;
    Quaternion initialRotationDifference;

    public bool isMouth = false;


    // Start is called before the first frame update
    void Start()
    {
        otherSkeletonRoot = transform.root.GetComponent<ObjectToCopy>().objectToCopy;
        // Ensure that the otherSkeletonRoot is assigned.
        if (otherSkeletonRoot == null)
        {
            Debug.LogError("Please assign the other skeleton's root GameObject to otherSkeletonRoot.");
            return;
        }

        // Find the matching limb in the other skeleton.
        string limbName = transform.name; // Assuming the limb names are the same in both skeletons.
        matchingLimb = FindMatchingLimb(otherSkeletonRoot, limbName);


        // Do something with the matchingLimb (e.g., store it, use it for IK, etc.).
        if (matchingLimb != null)
        {
            targetLimb = matchingLimb;
            // Example: You can parent this limb to the matching limb for synchronization.
            //transform.SetParent(matchingLimb);
        }
        else
        {
            Debug.LogWarning("No matching limb found in the other skeleton.");
        }



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

            if (isMouth)
            {
                transform.localPosition = targetLimb.localPosition;
            }
        }
        else
        {
            Debug.LogError("Joint's connected body is null.");
        }
    }

    private Transform FindMatchingLimb(Transform root, string limbName)
    {
        // Recursive function to search for a limb with the specified name.
        foreach (Transform child in root)
        {
            if (child.name == limbName)
            {
                return child;
            }

            // Search children of this child.
            Transform matchingChild = FindMatchingLimb(child, limbName);
            if (matchingChild != null)
            {
                return matchingChild;
            }
        }

        // No matching limb found.
        return null;
    }

}


