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
    }

    public void DisplayRandomText()
    {
        // Reset damageEnemy and defendEnemy to initial values
        HealthManager.Instance.damageEnemy = initialDamageEnemy;

        int randomIndex = Random.Range(0, randomText.Length);
        string selectedText = randomText[randomIndex];
        tmpRandomEnemy.text = selectedText;

        Debug.Log($"Selected Text: {selectedText}, Index: {randomIndex}");

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
