using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{

    public Transform targetTransform;

    public Vector3 targetOffset = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        transform.position = targetTransform.position + targetOffset;
    }
}
