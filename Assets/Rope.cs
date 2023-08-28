using UnityEngine;

public class Rope : MonoBehaviour
{
    public GameObject startObject;
    public GameObject endObject;
    public GameObject ropeSegmentPrefab;

    public int numSegments = 10;
    public float segmentLength = 0.2f;

    private Transform[] ropeSegments;

    private void Start()
    {
        InitializeRope();
    }

    private void InitializeRope()
    {
        ropeSegments = new Transform[numSegments];

        for (int i = 0; i < numSegments; i++)
        {
            GameObject segment = Instantiate(ropeSegmentPrefab, transform);
            ropeSegments[i] = segment.transform;

            if (i == 0)
            {
                segment.transform.position = startObject.transform.position;
            }
            else if (i == numSegments - 1)
            {
                segment.transform.position = endObject.transform.position;
            }
            else
            {
                segment.transform.position = Vector3.Lerp(startObject.transform.position, endObject.transform.position, (float)i / (numSegments - 1));
            }

            if (i > 0)
            {
                // Create Hinge Joint to connect the segments
                HingeJoint hingeJoint = segment.AddComponent<HingeJoint>();
                hingeJoint.connectedBody = ropeSegments[i - 1].GetComponent<Rigidbody>();
                hingeJoint.axis = Vector3.forward;
                hingeJoint.autoConfigureConnectedAnchor = false;
                hingeJoint.connectedAnchor = Vector3.zero;
            }
        }
    }
}