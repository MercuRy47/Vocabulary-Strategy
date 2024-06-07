using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionsRespon : MonoBehaviour
{
    public TextMeshProUGUI tmpScore;

    public TextMeshProUGUI tmpQuestion;
    public TextMeshProUGUI tmpOption1;
    public TextMeshProUGUI tmpOption2;
    public TextMeshProUGUI tmpOption3;

    private List<int> usedNumbers = new List<int>();
    private int maxNumber = 29;
    private int saveNumber, number1, number2;
    public static string correctAnswer;
    public string answerRandom1;
    public string answerRandom2;

    private LoadQuestions loadQuestions;

    private void Start()
    {
        loadQuestions = FindObjectOfType<LoadQuestions>();
        loadQuestions.LoadJson(); // âËÅ´¢éÍÁÙÅ JSON ä¿Åì
        randomQuestion();
    }

    private void Update()
    {
        tmpScore.SetText("Correct: " + CheckAnswer.correctScore + "\nWrong: " + CheckAnswer.wrongScore);
    }

    public void randomQuestion()
    {
        GenerateRandomNumbers();
        string id = loadQuestions.questionsList.questions[saveNumber].id;
        string question = loadQuestions.questionsList.questions[saveNumber].question;
        correctAnswer = loadQuestions.questionsList.questions[saveNumber].answer;

        GenerateRandomAnswers();
        answerRandom1 = loadQuestions.questionsList.questions[number1].answer;
        answerRandom2 = loadQuestions.questionsList.questions[number2].answer;

        Debug.Log("ID: " + id + " Question: " + question + " Answer: " + correctAnswer);

        // Generate random indices for the options
        int[] optionIndices = { 0, 1, 2 };
        Shuffle(optionIndices); // Shuffle the indices

        // Assign the answers to the options
        tmpQuestion.SetText(question);
        tmpOption1.SetText(GetAnswer(optionIndices[0]));
        tmpOption2.SetText(GetAnswer(optionIndices[1]));
        tmpOption3.SetText(GetAnswer(optionIndices[2]));
    }

    private string GetAnswer(int index)
    {
        if (index == 0)
        {
            return answerRandom1;
        }
        else if (index == 1)
        {
            return answerRandom2;
        }
        else
        {
            return correctAnswer;
        }
    }

    private void Shuffle(int[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }

    private void GenerateRandomNumbers()
    {
        if (usedNumbers.Count >= maxNumber)
        {
            usedNumbers.Clear(); // Clear used numbers if all numbers are used
            //return; // Exit the function if all numbers are used
        }

        int randomNumber;
        do
        {
            randomNumber = Random.Range(0, maxNumber + 1);
        } while (usedNumbers.Contains(randomNumber));

        usedNumbers.Add(randomNumber);
        saveNumber = randomNumber;
    }



    private void GenerateRandomAnswers()
    {
        List<int> numbers = new List<int>();
        for (int i = 0; i < loadQuestions.questionsList.questions.Count; i++)
        {
            if (i != saveNumber)
            {
                numbers.Add(i);
            }
        }

        number1 = numbers[Random.Range(0, numbers.Count)];
        numbers.Remove(number1);

        number2 = numbers[Random.Range(0, numbers.Count)];
    }
}
