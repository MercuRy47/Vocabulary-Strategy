using System.Collections;
using UnityEngine;

public class LogoPop : MonoBehaviour
{
    public float popDuration = 0.5f;
    public float popScale = 1.5f;
    public float popDelay = 1f;

    private Vector3 originalScale;
    private Coroutine currentCoroutine;

    private void Start()
    {
        originalScale = transform.localScale;
        StartCoroutine(PopRoutine());
    }

    private IEnumerator PopRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(popDelay);

            Debug.Log("Start popping animation");
            currentCoroutine = StartCoroutine(PopAnimation());
            yield return currentCoroutine;
            Debug.Log("End popping animation");
            yield return new WaitForSeconds(popDelay);
        }
    }

    private IEnumerator PopAnimation()
    {
        float timer = 0f;

        while (timer < popDuration)
        {
            float scaleFactor = Mathf.Lerp(1f, popScale, timer / popDuration);
            transform.localScale = originalScale * scaleFactor;
            timer += Time.deltaTime;
            yield return null;
        }

        transform.localScale = originalScale * popScale;

        timer = 0f;

        while (timer < popDuration)
        {
            float scaleFactor = Mathf.Lerp(popScale, 1f, timer / popDuration);
            transform.localScale = originalScale * scaleFactor;
            timer += Time.deltaTime;
            yield return null;
        }

        transform.localScale = originalScale;
    }


    private void OnDisable()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
    }
}
