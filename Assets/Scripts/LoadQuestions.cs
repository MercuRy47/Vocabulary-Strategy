using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadQuestions : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string id;
        public string question;
        public string answer;
    }

    [System.Serializable]
    public class QuestionsList
    {
        public List<Question> questions;
    }

    public TextAsset jsonFileName; // Drag and drop the JSON file here in the Inspector
    public QuestionsList questionsList;

    void Start()
    {
        LoadJson();
        DisplayQuestion(0); // ตัวอย่าง: แสดงคำถามแรก
    }

    public void LoadJson()
    {
        if (jsonFileName != null)
        {
            questionsList = JsonUtility.FromJson<QuestionsList>(jsonFileName.text);
        }
        else
        {
            Debug.LogError("ไม่พบไฟล์ JSON!");
        }
    }

    private void DisplayQuestion(int index)
    {
        if (questionsList != null && index >= 0 && index < questionsList.questions.Count)
        {
            Question question = questionsList.questions[index];
            //Debug.Log("Question ID: " + question.id);
            //Debug.Log("Question: " + question.question);
            //Debug.Log("Answer: " + question.answer);
        }
        else
        {
            Debug.LogError("Index ไม่ถูกต้องหรือ questions list เป็น null!");
        }
    }
}
