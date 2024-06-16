using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SummarizeManager : MonoBehaviour
{
    public static SummarizeManager Instance { get; private set; }

    public GameObject stars;
    public TextMeshProUGUI summarizeTMP;
    public TextMeshProUGUI summarize2TMP;

    private StarRating script1;

    private int correctScore;
    private int wrongScore;
    private int totalQuestions;
    private float timeSpent;

    public static int currentStar;

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
        script1 = stars.GetComponent<StarRating>();
    }

    private void Update()
    {
        //CalScore();
    }

    public void CalScore()
    {
        correctScore = CheckAnswer.correctScore;
        wrongScore = CheckAnswer.wrongScore;
        totalQuestions = CheckAnswer.correctScore + CheckAnswer.wrongScore;
        timeSpent = Timer.Instance.elapsedTime;

        int finalScore = CalculateScore();
        int stars = CalculateStars(finalScore);
        currentStar = stars;

        if (HealthManager.Instance.healthPlayer <= 0)
        {
            stars = 0;
            summarize2TMP.text = $"Score: {finalScore} \nRecord: {Timer.Instance.elapsedTime.ToString("F2")} \nCorrect Answer: {CheckAnswer.correctScore} \nWrong Answer: {CheckAnswer.wrongScore}";
        }
        if (HealthManager.Instance.healthEnemy <= 0)
        {
            script1.count = stars;
            summarizeTMP.text = $"Score: {finalScore} \nRecord: {Timer.Instance.elapsedTime.ToString("F2")} \nCorrect Answer: {CheckAnswer.correctScore} \nWrong Answer: {CheckAnswer.wrongScore}";
            CheckScenes();
        }

    }

    public int CalculateScore()
    {
        int baseScore = correctScore * 10;
        int penaltyScore = wrongScore * -5;
        float averageTimePerQuestion = timeSpent / totalQuestions;

        float timePenalty = 0;
        if (averageTimePerQuestion > 3)
        {
            timePenalty = (averageTimePerQuestion - 3) * totalQuestions * -2;
        }

        int totalScore = Mathf.Max(0, (int)(baseScore + penaltyScore + timePenalty));
        return totalScore;
    }

    public int CalculateStars(int score)
    {
        int maxPossibleScore = totalQuestions * 10; // คำนวณคะแนนเต็มจากจำนวนคำตอบที่ถูกต้องทั้งหมด
        float correctAnswerPercentage = (float)correctScore / totalQuestions;

        if (correctAnswerPercentage >= 0.8f)
        {
            return 3; // 3 ดาวถ้าคะแนน >= 80% ของคะแนนเต็ม
        }
        else if (correctAnswerPercentage >= 0.5f)
        {
            return 2; // 2 ดาวถ้าคะแนน >= 50% ของคะแนนเต็ม
        }
        else
        {
            return 1; // 1 ดาวถ้าคะแนน < 50% ของคะแนนเต็ม
        }
    }

    public void CheckScenes()
    {
        if (HealthManager.Instance.healthPlayer <= 0) return;

        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "State-1")
        {
            if (StarRatingManager.starCounts[0] > currentStar) return;
            StarRatingManager.AddToStarCount(0, currentStar);
            if (LoadScenes.countState > 1) return;
            LoadScenes.countState++;
        }
        else if (currentScene.name == "State-2")
        {
            if (StarRatingManager.starCounts[1] > currentStar) return;
            StarRatingManager.AddToStarCount(1, currentStar);
            if (LoadScenes.countState > 2) return;
            LoadScenes.countState++;
        }
        SaveGame.SaveCountState();
    }
}
