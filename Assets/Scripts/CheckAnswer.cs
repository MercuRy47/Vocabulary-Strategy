using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheckAnswer : MonoBehaviour
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

    private void Update()
    {
        correctAnswer = QuestionsRespon.correctAnswer;
        optionAnswer = targetOption.text;
    }

    public void checkAnswer()
    {
        StartCoroutine(DelayThenReset());
    }

    private IEnumerator DelayThenReset()
    {
        QuestionsRespon questionsRespon = FindObjectOfType<QuestionsRespon>();

        if (optionAnswer == correctAnswer)
        {
            targetImage.sprite = correct;
            correctScore++;
        }
        else
        {
            targetImage.sprite = wrong;
            wrongScore++;
        }

        yield return new WaitForSeconds(1f);
        targetImage.sprite = non;

        yield return new WaitForSeconds(0.5f);
        questionsRespon.randomQuestion();

        Debug.Log(optionAnswer + " : " + correctAnswer);
    }
}
