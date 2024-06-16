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
    public static int comboScore;

    private void Update()
    {
        correctAnswer = QuestionsRespon.correctAnswer;
        optionAnswer = targetOption.text;
    }

    private void Start()
    {
        Timer.Instance.elapsedTime = 0f;
        correctScore = 0;
        wrongScore = 0;
        comboScore = 0;
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
            comboScore++;

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
            targetImage.sprite = wrong;
            wrongScore++;
            comboScore = 0;
            UIManager.currentCoin += 150 * UIManager.CoinBonus;
        }

        yield return new WaitForSeconds(0.5f);
        targetImage.sprite = non;

        yield return new WaitForSeconds(0.25f);
        questionsRespon.randomQuestion();

        Debug.Log(optionAnswer + " : " + correctAnswer);
        Debug.Log(comboScore);
    }
}
