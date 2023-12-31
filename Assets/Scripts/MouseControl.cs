using UnityEngine;

public class MouseControl : MonoBehaviour
{
    public float moveSpeed = 5f;    // Speed of movement
    public float boundary = 5f;     // Distance from screen center to boundaries
    public float smoothTime = 0.1f; // Smoothing time for interpolation

    private Rigidbody rb;
    private Vector3 currentVelocity = Vector3.zero;
    private Vector3 previousPosition;
    private Vector3 targetPosition;

    AudioSource mouseNoise;
    ParticleSystem mouseSparks;
    public int sparkEmissionFactor = 10;

    float normalizedMouseSpeed;

    public float acceleration = 10;

    private void Start()
    {
        Cursor.visible = false;
        Physics.IgnoreLayerCollision(6, 7, true);

        rb = GetComponent<Rigidbody>();
        mouseNoise = GetComponent<AudioSource>();
        previousPosition = rb.position;
        mouseSparks = transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        UpdateMovement();
        normalizedMouseSpeed = GetNormalizedMouseSpeed();
        UpdateMouseVolume();
        UpdateSparkEmissionRate();
    }

    float GetNormalizedMouseSpeed()
    {
        // Calculate mouse speed
        float mouseSpeed = (rb.position - previousPosition).magnitude / Time.deltaTime;

        // Normalize mouse speed to a value between 0 and 1
        float normalizedMouseSpeed = Mathf.Clamp01(mouseSpeed / moveSpeed);

        return normalizedMouseSpeed;
    }

    void UpdateSparkEmissionRate()
    {
        // Update the particle effect rate based on normalized mouse speed
        var particleEmission = mouseSparks.emission;
        if (normalizedMouseSpeed > .2f)
        {
            particleEmission.enabled = true;
            particleEmission.rateOverTime = normalizedMouseSpeed * sparkEmissionFactor;
        }
        else
        {
            particleEmission.enabled = false;
        }
    }

    void UpdateMouseVolume()
    {
        // Update the audio volume based on normalized mouse speed
        mouseNoise.volume = normalizedMouseSpeed;
        previousPosition = rb.position;
    }


    /*
    void UpdateMovement()
    {
        // Get mouse position in world coordinates
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));

        // Calculate clamped target position based on mouse position and boundaries
        float clampedX = Mathf.Clamp(mousePos.x, -boundary, boundary);
        float clampedZ = Mathf.Clamp(mousePos.z, -boundary, boundary);
        Vector3 targetPosition = new Vector3(clampedX, 0f, clampedZ);

        // Raycast to check for obstacles before moving
        Vector3 moveDirection = targetPosition - rb.position;
        RaycastHit hit;
        if (Physics.Raycast(rb.position, moveDirection, out hit, moveDirection.magnitude))
        {
            // Adjust target position to avoid obstacles
            targetPosition = hit.point - moveDirection.normalized * 0.1f; // Move slightly away from the obstacle
        }




        if (Vector3.Distance(transform.position, targetPosition) < .1f)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            // Smoothly lerp to the target position
            rb.position = Vector3.SmoothDamp(rb.position, targetPosition, ref currentVelocity, smoothTime, moveSpeed);

            // Optional: Keep the GameObject's y position unchanged
            rb.position = new Vector3(rb.position.x, 0f, rb.position.z);

            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }*/

    void UpdateMovement()
{
    // Get the aspect ratio of the screen
    float screenAspectRatio = (float)Screen.width / Screen.height;

    // Calculate the adjusted boundary based on the screen aspect ratio
    float adjustedBoundary = boundary * screenAspectRatio;

    // Get mouse position in world coordinates
    Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));


    // Calculate clamped target position based on mouse position and adjusted boundaries
    float clampedX = Mathf.Clamp(mousePos.x, -adjustedBoundary, adjustedBoundary);
    float clampedZ = Mathf.Clamp(mousePos.z, -boundary, boundary); // Assuming vertical boundaries remain unchanged
    Vector3 targetPosition = new Vector3(clampedX, 0f, clampedZ);

    // Raycast to check for obstacles before moving
    Vector3 moveDirection = targetPosition - rb.position;
    RaycastHit hit;
    if (Physics.Raycast(rb.position, moveDirection, out hit, moveDirection.magnitude))
    {
        // Adjust target position to avoid obstacles
        targetPosition = hit.point - moveDirection.normalized * 0.1f; // Move slightly away from the obstacle
    }

    if (Vector3.Distance(transform.position, targetPosition) < .1f)
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }
    else
    {
        // Smoothly lerp to the target position
        rb.position = Vector3.SmoothDamp(rb.position, targetPosition, ref currentVelocity, smoothTime, moveSpeed);

        // Optional: Keep the GameObject's y position unchanged
        rb.position = new Vector3(rb.position.x, 0f, rb.position.z);

        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }
}





}
