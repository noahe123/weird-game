using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    public Animator animator;
    private GameObject grabbedObj;
    private Rigidbody rb;
    public int isLeftOrRight;
    private bool isGrabbing = false;

    FixedJoint fj;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.IgnoreLayerCollision(8, 8);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(isLeftOrRight) && !isGrabbing)
        {
            isGrabbing = true;
            if (isLeftOrRight == 0)
            {
                animator.SetBool("isGrabLeft", true);
            }
            else if (isLeftOrRight == 1)
            {
                animator.SetBool("isGrabRight", true);
            }

            grabbedObj = null;


        }
        else if (Input.GetMouseButtonUp(isLeftOrRight) && isGrabbing)
        {
            isGrabbing = false;
            if (isLeftOrRight == 0)
            {
                animator.SetBool("isGrabLeft", false);
            }
            else if (isLeftOrRight == 1)
            {
                animator.SetBool("isGrabRight", false);
            }

            if (fj != null)
            {
                Destroy(fj);
            }

            grabbedObj = null;


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isGrabbing)
        {
            if (other.gameObject.CompareTag("Item"))
            {
                grabbedObj = other.gameObject;

                fj = grabbedObj.AddComponent<FixedJoint>();
                fj.connectedBody = rb;
                fj.breakForce = 9001;
            }

            Debug.Log("colliding");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("exiting");

        if (fj != null)
        {
            Destroy(fj);
        }


    }
}
