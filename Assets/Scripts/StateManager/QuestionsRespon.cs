using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionsRespon : MonoBehaviour
{
    public static QuestionsRespon Instance { get; private set; }

    public GameObject questionsOn;
    public GameObject timerOn;
    public GameObject questionsOff;
    public GameObject descriptionPage;

    public TextMeshProUGUI tmpScore;
    public TextMeshProUGUI tmpScoreBar;

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
    public int times; // Set this to the number of times randomQuestion() should be called
    public int questionCount = 0; // Counter for the number of times randomQuestion() has been called

    private LoadQuestions loadQuestions;

    private void Awake()
    {
        // Make sure there is only one instance of QuestionsRespon
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

    private void Start()
    {
        descriptionPage.SetActive(true);
        loadQuestions = FindObjectOfType<LoadQuestions>();
        loadQuestions.LoadJson(); // Load the JSON file
        //randomQuestion();
        //CountTime.Instance.StartCountdown();
    }

    private void Update()
    {
        tmpScore.SetText("Correct: " + CheckAnswer.correctScore + "\nWrong: " + CheckAnswer.wrongScore);
        Debug.Log(questionCount + " : " + times);

    }

    public void runStart()
    {
        descriptionPage.SetActive(false);
        if (CountStart.Instance != null)
        {
            CountStart.Instance.StartCountdown();
        }
        else
        {
            Debug.LogError("CountStart.Instance is null");
        }
    }
    public void runTimer()
    {
        Timer.Instance.StartCountTimer();
    }

    public void runRandom()
    {
        
        if (questionCount == times)
        {
            questionsOn.SetActive(false);
            timerOn.SetActive(false);
            questionsOff.SetActive(true);

            return;
        }
        randomQuestion();
    }


    public void randomQuestion()
    {
        if (questionCount == times)
        {
            questionsOn.SetActive(false);
            timerOn.SetActive(false);
            questionsOff.SetActive(true);
            CountTime.Instance.ResetCountdown();
            Timer.Instance.StopCountTimer();
            questionCount = 0;
            return;
        }

        // Show questionsOn and timerOn after setting up the question
        questionsOn.SetActive(true);
        timerOn.SetActive(true);
        questionsOff.SetActive(false);
        CountTime.Instance.StartCountdown();
        runTimer();

        // Generate a new random number that hasn't been used yet
        saveNumber = GetUnusedRandomNumber();

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

        questionCount++; // Increment the counter
    }

    private int GetUnusedRandomNumber()
    {
        int randomNumber;
        do
        {
            randomNumber = Random.Range(0, maxNumber + 1);
        } while (usedNumbers.Contains(randomNumber));

        usedNumbers.Add(randomNumber);
        return randomNumber;
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
