using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed, strafeSpeed, jumpForce;
    public float minDistanceToTarget = 6.0f; // Minimum distance to start moving towards the target.

    public Rigidbody hips;
    public bool isGrounded;

    public Transform target; // The target object to move towards.

    public Animator animator;

    public Transform root;

    public Transform player;

    public ConfigurableJoint hipJoint, headJoint;


    public float rotationSmoothTime;

    public Vector3 desiredRot;

    // Start is called before the first frame update
    void Start()
    {
        hips = GetComponent<Rigidbody>();
        root = FindObjectOfType<CameraControl>().root;
    }
    
    private void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetBool("isWalk", true);
                animator.SetBool("isRun", true);

                hips.AddForce(hips.transform.forward * speed * 1.5f);
            }
            else
            {
                animator.SetBool("isRun", false);
                animator.SetBool("isWalk", true);
                hips.AddForce(hips.transform.forward * speed);
            }
        }
        else
        {
            animator.SetBool("isWalk", false);
            animator.SetBool("isRun", false);

        }

        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("isWalkBack", true);
            hips.AddForce(-hips.transform.forward * speed);
        }
        else if (!Input.GetKey(KeyCode.W))
        {
            animator.SetBool("isWalkBack", false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("isSideLeft", true);
            hips.AddForce(-hips.transform.right * strafeSpeed);
        }
        else
        {
            animator.SetBool("isSideLeft", false);

        }

        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isSideRight", true);
            hips.AddForce(hips.transform.right * strafeSpeed);
        }
        else
        {
            animator.SetBool("isSideRight", false);
        }



        if (Input.GetAxis("Jump") > 0)
        {
            if (isGrounded)
            {
                hips.AddForce(new Vector3(0, jumpForce, 0));
                isGrounded = false;
            }
        }
    }


  /*  private void FixedUpdate()
    {
        // Calculate the direction from the player's hips to the target.
        Vector3 targetDirection = (target.position - hips.position).normalized;


        Vector3 directionToTarget = target.position - transform.position;

        directionToTarget.Normalize();

        root.rotation = Quaternion.LookRotation(directionToTarget);


        Quaternion desiredRot = Quaternion.LookRotation(directionToTarget);

        hipJoint.targetRotation = Quaternion.Euler(new Vector3(0, -desiredRot.eulerAngles.y, 0));


        // Calculate the distance between the player's hips and the target.
        float distanceToTarget = Vector3.Distance(hips.position, target.position);

        // Check if the target is within the minimum distance to start moving towards it.
        if (distanceToTarget >= minDistanceToTarget)
        {

            // Move the player's hips towards the target.
            hips.AddForce(targetDirection * speed);



            animator.SetBool("isWalk", true);
            animator.SetBool("isRun", true);


        }
        else
        {
            animator.SetBool("isWalk", false);
            animator.SetBool("isRun", false);

        }

        // Rest of your movement code goes here...

        if (Input.GetAxis("Jump") > 0)
        {
            if (isGrounded)
            {
                hips.AddForce(new Vector3(0, jumpForce, 0));
                isGrounded = false;
            }
        }
    }
  */

}
