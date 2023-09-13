using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbCollision : MonoBehaviour
{
    public Renderer playerRenderer;
    public PlayerController playerController;

    private Coroutine damageEffectCoroutine;

    Color[] originalColors;

    void Start()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>().GetComponent<PlayerController>();

        playerRenderer = transform.root.GetChild(0).GetComponent<Renderer>();

        // Get the current colors of the materials
        originalColors = new Color[playerRenderer.materials.Length];
        for (int i = 0; i < playerRenderer.materials.Length; i++)
        {
            originalColors[i] = playerRenderer.materials[i].color;
        }

        // Find the renderer in the child objects if not assigned in the Inspector
        if (playerRenderer == null)
        {
            playerRenderer = GetComponentInChildren<Renderer>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != 9)
        {
            playerController.isGrounded = true;
        }

        if (collision.gameObject.layer == 9)
        {
            if (collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 20)
            {
                // Cancel any existing damage effect coroutine and start a new one
                if (damageEffectCoroutine != null)
                {
                    StopCoroutine(damageEffectCoroutine);
                }
                damageEffectCoroutine = StartCoroutine(DamageEffect());
            }
        }
    }

    IEnumerator DamageEffect()
    {



        // Tint the material red
        Color targetColor = Color.red;
        float duration = 0.3f; // You can adjust the duration as needed

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float lerpFactor = t / duration;

            // Lerp the material color
            for (int i = 0; i < playerRenderer.materials.Length; i++)
            {
                playerRenderer.materials[i].color = Color.Lerp(originalColors[i], targetColor, lerpFactor);
            }

            yield return null;
        }

        // Ensure the final color is set to targetColor
        for (int i = 0; i < playerRenderer.materials.Length; i++)
        {
            playerRenderer.materials[i].color = targetColor;
        }

        // Wait for a brief moment
        yield return new WaitForSeconds(0.1f);

        // Revert the material color back to the original
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float lerpFactor = t / duration;

            // Lerp the material color back to the original color
            for (int i = 0; i < playerRenderer.materials.Length; i++)
            {
                playerRenderer.materials[i].color = Color.Lerp(targetColor, originalColors[i], lerpFactor);
            }

            yield return null;
        }

        // Ensure the material color is back to the original color
        for (int i = 0; i < playerRenderer.materials.Length; i++)
        {
            playerRenderer.materials[i].color = originalColors[i];
        }
    }
}
