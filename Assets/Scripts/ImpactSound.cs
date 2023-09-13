using UnityEngine;

public class ImpactSound : MonoBehaviour
{
    public AudioSource audioSource;
    public float minVelocity = 1.0f; // Minimum velocity to trigger sound
    public float maxVelocity = 10.0f; // Maximum velocity for full volume
    public AudioClip[] impactSounds;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        float impactVelocity = collision.relativeVelocity.magnitude;

        if (impactVelocity >= minVelocity)
        {
            // Calculate the normalized volume based on impact velocity
            float normalizedVolume = Mathf.InverseLerp(minVelocity, maxVelocity, impactVelocity/2);

            // Play the audio clip with the calculated volume
            audioSource.PlayOneShot(impactSounds[Random.Range(0, impactSounds.Length)], normalizedVolume/5);
        }
    }
}
