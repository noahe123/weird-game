using System.Collections;
using UnityEngine;

public class MoveTowardsMouse : MonoBehaviour
{
    public float moveSpeed = 5f;
    public LayerMask groundLayer; // Set this to the layer(s) your ground objects are on.

    private Rigidbody rb;
    private Vector3 targetPosition;
    private bool isMoving = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check if the left mouse button is pressed.
        if (Input.GetMouseButton(0))
        {
            // Cast a ray from the mouse position into the world.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                // Set the target position to the hit point.
                targetPosition = hit.point;
                isMoving = true;
            }
        }
    }

    void FixedUpdate()
    {
        // If we have a target position, move towards it.
        if (isMoving)
        {
            // Calculate the direction to the target.
            Vector3 moveDirection = (targetPosition - rb.position).normalized;

            // Calculate the desired velocity.
            Vector3 desiredVelocity = moveDirection * moveSpeed;

            // Apply a force to the Rigidbody.
            rb.AddForce(desiredVelocity * rb.mass, ForceMode.Force);

            // If the distance to the target is very small, stop moving.
            if (Vector3.Distance(rb.position, targetPosition) < 0.1f)
            {
                isMoving = false;
            }
        }
    }
}
