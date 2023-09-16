using UnityEngine;

public class FindMatchingChild : MonoBehaviour
{
    public Transform otherSkeletonRoot; // Assign the root GameObject of the other skeleton in the Inspector.

    public Transform matchingLimb;

    private void Start()
    {
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
            // Example: You can parent this limb to the matching limb for synchronization.
            transform.SetParent(matchingLimb);
        }
        else
        {
            Debug.LogWarning("No matching limb found in the other skeleton.");
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
