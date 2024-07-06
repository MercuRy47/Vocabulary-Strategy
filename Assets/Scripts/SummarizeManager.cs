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
            summarize2TMP.text = $"Score: {finalScore} \nRecord: {timeSpent.ToString("F2")} \nCorrect Answer: {CheckAnswer.correctScore} \nWrong Answer: {CheckAnswer.wrongScore}";
        }
        if (HealthManager.Instance.healthEnemy <= 0)
        {
            script1.count = stars;
            summarizeTMP.text = $"Score: {finalScore} \nRecord: {timeSpent.ToString("F2")} \nCorrect Answer: {CheckAnswer.correctScore} \nWrong Answer: {CheckAnswer.wrongScore}";
            CheckScenes();
        }
    }

    public int CalculateScore()
    {
        int baseScore = (int)(((float)correctScore / totalQuestions) * 100);
        return baseScore;
    }

    public int CalculateStars(int score)
    {
        if (score >= 80)
        {
            return 3; // 3 stars if score >= 80
        }
        else if (score >= 50)
        {
            return 2; // 2 stars if score >= 50
        }
        else
        {
            return 1; // 1 star if score < 50
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
        else if (currentScene.name == "State-3")
        {
            if (StarRatingManager.starCounts[2] > currentStar) return;
            StarRatingManager.AddToStarCount(2, currentStar);
            if (LoadScenes.countState > 3) return;
            LoadScenes.countState++;
        }
        else if (currentScene.name == "State-4")
        {
            if (StarRatingManager.starCounts[3] > currentStar) return;
            StarRatingManager.AddToStarCount(3, currentStar);
            if (LoadScenes.countState > 4) return;
            LoadScenes.countState++;
        }
        else if (currentScene.name == "State-5")
        {
            if (StarRatingManager.starCounts[4] > currentStar) return;
            StarRatingManager.AddToStarCount(4, currentStar);
            if (LoadScenes.countState > 5) return;
            LoadScenes.countState++;
        }
        else if (currentScene.name == "State-6")
        {
            if (StarRatingManager.starCounts[5] > currentStar) return;
            StarRatingManager.AddToStarCount(5, currentStar);
            if (LoadScenes.countState > 6) return;
            LoadScenes.countState++;
        }
        else if (currentScene.name == "State-7")
        {
            if (StarRatingManager.starCounts[6] > currentStar) return;
            StarRatingManager.AddToStarCount(6, currentStar);
            if (LoadScenes.countState > 7) return;
            LoadScenes.countState++;
        }
        SaveGame.SaveCountState();
    }
}
