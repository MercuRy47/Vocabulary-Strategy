using System.Collections;
using UnityEngine;

public class FadeInObjects : MonoBehaviour
{
    public GameObject objectToFade;
    public float fadeDuration = 1.0f;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Start()
    {
        spriteRenderer = objectToFade.GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        StartCoroutine(FadeObject());
    }

    private IEnumerator FadeObject()
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(originalColor.a, 0.0f, elapsedTime / fadeDuration);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // Disable the object
        objectToFade.SetActive(false);
    }
}
