using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomEnemy : MonoBehaviour
{
    public static RandomEnemy Instance { get; private set; }

    public TextMeshProUGUI tmpRandomEnemy;

    private static string[] randomText = { "-Non-", "Enemy: Attack x1.5", "-Non-", "Enemy: Attack x1.2", "-Non-", "-Non-", "Enemy: Health +20", "-Non-", "-Non-", "-Non-" };

    public float initialDamageEnemy;
    public float displayTime = 2f; // เวลาที่ใช้แสดงข้อความสุ่ม

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
        // Store initial values of damageEnemy and defendEnemy
        initialDamageEnemy = HealthManager.Instance.damageEnemy;
        // Uncomment this line if you want to start displaying random text automatically
        // StartCoroutine(DisplayRandomTextRoutine()); 
    }

    public void DisplayRandomText()
    {
        StartCoroutine(DisplayRandomTextRoutine());
    }

    private IEnumerator DisplayRandomTextRoutine()
    {
        float elapsedTime = 0f;
        while (elapsedTime < displayTime)
        {
            int randomIndex = Random.Range(0, randomText.Length);
            string selectedText = randomText[randomIndex];
            tmpRandomEnemy.text = selectedText;

            yield return new WaitForSeconds(0.1f); // แสดงข้อความแบบสุ่มทุกๆ 0.1 วินาที
            elapsedTime += 0.1f;
        }

        // แสดงข้อความสุดท้ายที่สุ่มได้
        int finalRandomIndex = Random.Range(0, randomText.Length);
        string finalSelectedText = randomText[finalRandomIndex];
        tmpRandomEnemy.text = finalSelectedText;

        // ใช้ข้อความสุดท้ายที่สุ่มได้ในการคำนวณค่าอื่นๆ
        Debug.Log($"Selected Text: {finalSelectedText}, Index: {finalRandomIndex}");

        // ใช้ผลลัพธ์ที่ได้ในการปรับค่าอื่นๆ
        ApplyRandomEffect(finalRandomIndex);
    }

    private void ApplyRandomEffect(int randomIndex)
    {
        HealthManager.Instance.damageEnemy = initialDamageEnemy;

        if (randomIndex == 1)
        {
            HealthManager.Instance.damageEnemy = HealthManager.Instance.damageEnemy * 1.5f;
        }
        else if (randomIndex == 3)
        {
            HealthManager.Instance.damageEnemy = HealthManager.Instance.damageEnemy * 1.2f;
        }
        else if (randomIndex == 6)
        {
            if (HealthManager.Instance.healthEnemy + 20 > HealthManager.Instance.maxHealthEnemy)
            {
                HealthManager.Instance.healthEnemy = HealthManager.Instance.maxHealthEnemy;
            }
            else
            {
                HealthManager.Instance.healthEnemy += 20;
            }
        }
    }
}