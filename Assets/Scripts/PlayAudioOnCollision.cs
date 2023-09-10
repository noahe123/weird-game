using UnityEngine;

public class PlayAudioOnCollision : MonoBehaviour
{
    public float impactVolumeScale = 0.2f; // Adjust the volume based on collision impact
    public AudioSource a1, a2, a3;

    private void Start()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Calculate collision impact
        float impactMagnitude = collision.relativeVelocity.magnitude;

        // Adjust volume based on impact
        float adjustedVolume = impactMagnitude * impactVolumeScale;
        adjustedVolume = Mathf.Clamp01(adjustedVolume)/2; // Ensure volume is between 0 and 1

        // Set the volume and play the audio
        int randomNum = Random.Range(0, 2);
        AudioSource collisionAudio;

        switch(randomNum)
        {
            case 0:
                collisionAudio = a1;
                break;
            case 1:
                collisionAudio = a2;
                break;
            case 2:
                collisionAudio = a3;
                break;
            default:
                collisionAudio = a1;
                break;
        }

        collisionAudio.volume = adjustedVolume;
        collisionAudio.Play();
    }
}
