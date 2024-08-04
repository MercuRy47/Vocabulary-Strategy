using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button), typeof(AudioSource))]
public class CheckAnswer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI targetOption;
    private string optionAnswer;
    private string correctAnswer;

    public Image targetImage;
    public Sprite non; 
    public Sprite correct; 
    public Sprite wrong;

    public static int correctScore;
    public static int wrongScore;
    public static int comboScore;

    [Header("Sound")]
    public AudioClip correctAnswerSound;
    public AudioClip wrongAnswerSound;
    private AudioSource audioSource;

    public float hoverScale = 1.05f;
    public AudioClip hoverSound;

    private Button button;
    private Vector3 originalScale;

    public static bool correctWrong = false;

    private void Update()
    {
        correctAnswer = QuestionsRespon.correctAnswer;
        optionAnswer = targetOption.text;
    }

    private void Start()
    {
        button = GetComponent<Button>();
        button.interactable = true;
        Timer.Instance.elapsedTime = 0f;
        correctScore = 0;
        wrongScore = 0;
        comboScore = 0;

        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.Stop();

        originalScale = transform.localScale;
    }

    public void checkAnswer()
    {
        button.interactable = false;
        StartCoroutine(DelayThenReset());
    }

    public IEnumerator DelayThenReset()
    {
        QuestionsRespon questionsRespon = QuestionsRespon.Instance; // Get the instance of QuestionsRespon

        if (optionAnswer == correctAnswer)
        {
            audioSource.clip = correctAnswerSound;
            audioSource.Play();

            targetImage.sprite = correct;
            correctScore++;
            comboScore++;
            correctWrong = true;

            if (comboScore == 5)
            {
                UIManager.currentCoin += 1000;
            }
            if (comboScore == 3)
            {
                UIManager.currentCoin += 500;
            }
            UIManager.currentCoin += 300 * UIManager.CoinBonus;
        }
        else
        {
            audioSource.clip = wrongAnswerSound;
            audioSource.Play();

            targetImage.sprite = wrong;
            wrongScore++;
            correctWrong = false;
            comboScore = 0;
            UIManager.currentCoin += 150 * UIManager.CoinBonus;
        }

        //yield return new WaitForSeconds(0.1f);

        yield return new WaitForSeconds(0.3f);
        targetImage.sprite = non;
        questionsRespon.randomQuestion();
        button.interactable = true;

        Debug.Log(optionAnswer + " : " + correctAnswer);
        Debug.Log(comboScore);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button.interactable)
        {
            transform.localScale = originalScale * hoverScale;
            audioSource.clip = hoverSound;
            audioSource.Play();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;
    }
}
