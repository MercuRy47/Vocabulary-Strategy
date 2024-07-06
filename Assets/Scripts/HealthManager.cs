using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance { get; private set; }

    [Header("Player")]
    public Image healthBarPlayer;
    public TextMeshProUGUI healthPlayerTMP;
    public string namePlayer;
    public float healthPlayer;
    public float maxHealthPlayer;
    public float damagePlayer;
    public float defendPlayer;
    public TextMeshProUGUI infoBarPlayer;

    [Header("Enemy")]
    public Image healthBarEnemy;
    public TextMeshProUGUI healthEnemyTMP;
    public string nameEnemy;
    public float healthEnemy;
    public float maxHealthEnemy;
    public float damageEnemy;
    public float defendEnemy;
    public TextMeshProUGUI infoBarEnemy;

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
        healthPlayer = maxHealthPlayer;
        healthEnemy = maxHealthEnemy;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            AttackPlayer(10f);
        }

        UpdateUI();
    }

    public void AttackPlayer(float amount)
    {
        healthPlayer -= amount;
        if (healthPlayer < 0) healthPlayer = 0;
    }
    public void AttackEnemy(float amount)
    {
        healthEnemy -= amount;
        if (healthEnemy < 0) healthEnemy = 0;
    }

    private void UpdateUI()
    {
        healthPlayerTMP.text = $"{namePlayer}: {(int)healthPlayer} / {maxHealthPlayer}";
        healthEnemyTMP.text = $"{nameEnemy}: {(int)healthEnemy} / {maxHealthEnemy}";

        healthBarPlayer.fillAmount = Mathf.Lerp(0, 1, healthPlayer / maxHealthPlayer);
        healthBarEnemy.fillAmount = Mathf.Lerp(0, 1, healthEnemy / maxHealthEnemy);

        infoBarEnemy.text = $"Attack: {(int)damageEnemy} \nDefend: {(int)defendEnemy}";
        infoBarPlayer.text = $"Attack: {(int)damagePlayer} \nDefend: {(int)defendPlayer}";
    }
}
