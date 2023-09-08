using UnityEngine;

public class ImpactSound : MonoBehaviour
{
    public AudioSource audioSource;
    public float minVelocity = 1.0f; // Minimum velocity to trigger sound
    public float maxVelocity = 10.0f; // Maximum velocity for full volume
    public AudioClip impactSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        impactSound = audioSource.clip;
    }

    private void OnCollisionEnter(Collision collision)
    {
        float impactVelocity = collision.relativeVelocity.magnitude;

        if (impactVelocity >= minVelocity)
        {
            // Calculate the normalized volume based on impact velocity
            float normalizedVolume = Mathf.InverseLerp(minVelocity, maxVelocity, impactVelocity);

            // Play the audio clip with the calculated volume
            audioSource.PlayOneShot(impactSound, normalizedVolume);
        }
    }
}
