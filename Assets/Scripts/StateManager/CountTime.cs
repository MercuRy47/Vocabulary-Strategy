using System.Collections;
using TMPro;
using UnityEngine;

public class CountTime : MonoBehaviour
{
    public static CountTime Instance { get; private set; }

    public TextMeshProUGUI tmpTimer;

    private float countdownTime = 10f;  // Starting countdown time (10 seconds)
    private Coroutine countdownCoroutine;  // Coroutine for the countdown

    private void Awake()
    {
        // Make sure there is only one instance of CountTime
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

    // Start the countdown
    public void StartCountdown()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }
        countdownCoroutine = StartCoroutine(Countdown());
    }

    // Reset the countdown timer
    public void ResetCountdown()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }
        countdownTime = 10f;
        tmpTimer.text = countdownTime.ToString("F2");  // Display the countdown time
    }

    // Coroutine for the countdown
    private IEnumerator Countdown()
    {
        float timeRemaining = countdownTime;
        while (timeRemaining > 0)
        {
            tmpTimer.text = timeRemaining.ToString("F2");  // Display the countdown time
            timeRemaining -= Time.deltaTime;
            yield return null;
        }

        // Countdown has reached 0
        tmpTimer.text = "0.00";  // Display when the time is up
        CheckAnswer.wrongScore++;

        // Check if the question limit has been reached
        if (QuestionsRespon.Instance != null && QuestionsRespon.Instance.questionCount <= QuestionsRespon.Instance.times)
        {
            QuestionsRespon.Instance.randomQuestion();           
        }
        else
        {
            StopCoroutine(countdownCoroutine);  // Stop the countdown coroutine
            countdownCoroutine = null;
        }

    }
}