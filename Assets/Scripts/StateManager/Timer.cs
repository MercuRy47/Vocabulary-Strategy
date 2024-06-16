using System.Collections;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer Instance { get; private set; }

    public TextMeshProUGUI tmpTimer;

    public float elapsedTime = 0f;  // Elapsed time
    private Coroutine countCoroutine;  // Coroutine for counting time

    private void Awake()
    {
        // Make sure there is only one instance of Timer
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // Check if the T key is pressed
        if (Input.GetKeyDown(KeyCode.T))
        {
            StopCountTimer();
        }

        // Check if the R key is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCountTimer();
        }
    }

    // Start the timer
    public void StartCountTimer()
    {
        if (countCoroutine != null)
        {
            StopCoroutine(countCoroutine);
        }
        countCoroutine = StartCoroutine(CountTimer());
    }

    // Stop the timer
    public void StopCountTimer()
    {
        if (countCoroutine != null)
        {
            StopCoroutine(countCoroutine);
            countCoroutine = null;
        }
    }

    // Get the elapsed time
    public float GetElapsedTime()
    {
        return elapsedTime;
    }

    // Coroutine for counting time
    private IEnumerator CountTimer()
    {
        while (true)
        {
            elapsedTime += Time.deltaTime;
            tmpTimer.text = "Time: " + elapsedTime.ToString("F2");  // Display the elapsed time
            yield return null;
        }
    }
}
