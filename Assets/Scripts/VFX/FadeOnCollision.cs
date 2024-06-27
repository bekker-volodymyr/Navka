using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOnCollision : MonoBehaviour
{
    public float fadeDuration = 1f; // Duration of the fade effect
    private List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
    private Coroutine fadeCoroutine;
    [SerializeField] private float targetOpacity = 0.2f;

    void Start()
    {
        // Get all SpriteRenderer components attached to this GameObject and its children
        spriteRenderers.AddRange(GetComponentsInChildren<SpriteRenderer>());

        // Check if any SpriteRenderer components were found
        if (spriteRenderers.Count == 0)
        {
            Debug.LogError("No SpriteRenderer components found on this GameObject or its children.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (IsPlayerOrNPC(other))
        {
            if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(FadeTo(targetOpacity));
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (IsPlayerOrNPC(other))
        {
            if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(FadeTo(1f));
        }
    }

    private bool IsPlayerOrNPC(Collider2D collider)
    {
        return collider.CompareTag("Player") || collider.CompareTag("NPC");
    }

    IEnumerator FadeTo(float targetOpacity)
    {
        float time = 0f;
        List<float> startOpacities = new List<float>();

        // Store the initial opacity of each SpriteRenderer
        foreach (var spriteRenderer in spriteRenderers)
        {
            startOpacities.Add(spriteRenderer.color.a);
        }

        while (time < fadeDuration)
        {
            time += Time.deltaTime;

            for (int i = 0; i < spriteRenderers.Count; i++)
            {
                if (spriteRenderers[i] != null)
                {
                    float alpha = Mathf.Lerp(startOpacities[i], targetOpacity, time / fadeDuration);
                    spriteRenderers[i].color = new Color(spriteRenderers[i].color.r, spriteRenderers[i].color.g, spriteRenderers[i].color.b, alpha);
                }
            }

            yield return null;
        }

        // Ensure the final opacity is set to the target value
        foreach (var spriteRenderer in spriteRenderers)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, targetOpacity);
            }
        }
    }
}
