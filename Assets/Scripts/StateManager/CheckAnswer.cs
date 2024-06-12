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
    public int comboScore;
    public static int saveComboScore;
    public static int successPoint;

    private void Update()
    {
        correctAnswer = QuestionsRespon.correctAnswer;
        optionAnswer = targetOption.text;
    }

    public void checkAnswer()
    {
        StartCoroutine(DelayThenReset());
    }

    public IEnumerator DelayThenReset()
    {
        QuestionsRespon questionsRespon = QuestionsRespon.Instance; // Get the instance of QuestionsRespon

        if (optionAnswer == correctAnswer)
        {
            targetImage.sprite = correct;
            correctScore++;
            if(correctScore > 1)
            {
                comboScore++;
                saveComboScore = comboScore;
            }
        }
        else
        {
            targetImage.sprite = wrong;
            wrongScore++;
            comboScore = 0;
        }

        yield return new WaitForSeconds(0.5f);
        targetImage.sprite = non;

        yield return new WaitForSeconds(0.25f);
        questionsRespon.randomQuestion();

        Debug.Log(optionAnswer + " : " + correctAnswer);
    }
}
