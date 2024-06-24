using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountStart : MonoBehaviour
{
    public static CountStart Instance { get; private set; }

    public TextMeshProUGUI tmpCountStart;
    public GameObject GameObj1;
    public GameObject GameObj2;

    [Header("Sound")]
    public AudioClip countSound;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // เริ่มต้นนับถอยหลังได้โดยการเรียก StartCountdown()
        GameObj2.SetActive(false);

        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.Stop();
    }

    public void StartCountdown()
    {
        GameObj1.SetActive(true);
        //GameObj2.SetActive(false);
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        int countdownTime = 3;

        audioSource.clip = countSound;
        audioSource.Play();
        while (countdownTime > 0)
        {
            tmpCountStart.text = countdownTime.ToString();
            tmpCountStart.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            yield return new WaitForSeconds(0.1f); // Wait for the scale effect to be noticeable
            tmpCountStart.transform.localScale = Vector3.one; // Reset to original scale
            yield return new WaitForSeconds(0.9f); // Wait for the rest of the second
            countdownTime--;
        }

        tmpCountStart.text = "START"; // Display "START"
        tmpCountStart.fontSize = 300; // Set font size to 300
        tmpCountStart.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        yield return new WaitForSeconds(1.25f); // Wait for 2 seconds
        tmpCountStart.transform.localScale = Vector3.one; // Reset to original scale
        tmpCountStart.fontSize = 100; // Reset font size to default (change 100 to the original font size if different)

        GameObj1.SetActive(false);
        GameObj2.SetActive(true);
        QuestionsRespon.Instance.runRandom();
        Timer.Instance.StartCountTimer();
        BackgroundMusic.Instance.PlayBackgroundMusic();
        Debug.Log("Start");
    }
}