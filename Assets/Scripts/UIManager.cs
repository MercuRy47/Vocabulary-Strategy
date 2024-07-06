using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Left")]
    public TextMeshProUGUI coinTMP;
    public TextMeshProUGUI statsTMP;

    [Header("Middle")]
    public GameObject middleButton1;
    public GameObject middleButton2;
    public GameObject middleButton3;

    [Header("Right")]
    public GameObject rightButton1;
    public TextMeshProUGUI rightButton1TMP;
    public GameObject rightButton2;
    public TextMeshProUGUI rightButton2TMP;
    public GameObject rightButton3;
    public TextMeshProUGUI rightButton3TMP;
    public GameObject rightButton4;
    public TextMeshProUGUI rightButton4TMP;
    public GameObject rightButton5;
    public TextMeshProUGUI rightButton5TMP;
    public GameObject rightButton6;
    public TextMeshProUGUI rightButton6TMP;

    [Header("Sound")]
    public AudioClip buySound;
    public AudioClip reviveSound;
    
    private AudioSource audioSource;
    private int price;
    public static float currentCoin;
    private int AtkLevel = 0;
    private int DefLevel = 0;
    private int PassiveLevel = 0;
    private float PassiveBuf = 1;
    private int CoinLevel = 0;
    public static float CoinBonus = 1f;

    public static int allLevel;

    private void Start()
    {
        currentCoin = 0;
        CoinBonus = 1;
        UpdateCoin();
        StartRightUI();

        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.Stop();
    }

    private void Update()
    {
        UpdateCoin();
        UpdateStats();
    }

    public void UpdateCoin()
    {
        coinTMP.text = $"{(int)currentCoin}$";
    }

    public void UpdateStats()
    {
        statsTMP.text = $"Correct: {CheckAnswer.correctScore} \nWrong: {CheckAnswer.wrongScore} \nAttack: {(int)HealthManager.Instance.damagePlayer} \nDefend: {(int)HealthManager.Instance.defendPlayer} \nPassive: Atk. x{PassiveBuf}/ Def. x{PassiveBuf} \nCoin Bonus: x{CoinBonus}";
    }

    public void StartRightUI()
    {
        rightButton1TMP.text = $"Atk. lv.{AtkLevel} \n{400}$";
        rightButton2TMP.text = $"Def. lv.{DefLevel} \n{400}$";
        rightButton3TMP.text = $"Passive lv.{PassiveLevel} \n{400}$";
        rightButton4TMP.text = $"+{20} HP \n{1000}$";
        rightButton5TMP.text = $"Coin lv.{CoinLevel} \n{400}$";
    }

    public void buyAttack()
    {
        if (AtkLevel == 5) return;

        if (AtkLevel == 0)
        {
            price = 400;
        }
        else if (AtkLevel == 1)
        {
            price = 500;
        }
        else if (AtkLevel == 2)
        {
            price = 600;
        }
        else if (AtkLevel == 3)
        {
            price = 700;
        }
        else if (AtkLevel == 4)
        {
            price = 800;
        }

        if (currentCoin < price) return;

        currentCoin -= price;
        audioSource.clip = buySound;
        audioSource.Play();
        AtkLevel++;
        HealthManager.Instance.damagePlayer += 2;
        UpdateCoin();

        if (AtkLevel == 5)
        {
            rightButton1TMP.text = "Atk. \nlv.Max";
        }
        else
        {
            int nextPrice = 400 + (AtkLevel * 100);
            rightButton1TMP.text = $"Atk. lv.{AtkLevel} \n{nextPrice}$";
        }
        allLevel += AtkLevel;
    }
    public void buyDefend()
    {
        if (DefLevel == 5) return;

        if (DefLevel == 0)
        {
            price = 400;
        }
        else if (DefLevel == 1)
        {
            price = 500;
        }
        else if (DefLevel == 2)
        {
            price = 600;
        }
        else if (DefLevel == 3)
        {
            price = 700;
        }
        else if (DefLevel == 4)
        {
            price = 800;
        }

        if (currentCoin < price) return;

        currentCoin -= price;
        audioSource.clip = buySound;
        audioSource.Play();
        DefLevel++;
        HealthManager.Instance.defendPlayer += 2;
        UpdateCoin();

        if (DefLevel == 5)
        {
            rightButton2TMP.text = "Def. \nlv.Max";
        }
        else
        {
            int nextPrice = 400 + (DefLevel * 100);
            rightButton2TMP.text = $"Def. lv.{DefLevel} \n{nextPrice}$";
        }
        allLevel += DefLevel;
    }
    public void buyPassive()
    {
        if (PassiveLevel == 5) return;

        if (PassiveLevel == 0)
        {
            price = 400;
        }
        else if (PassiveLevel == 1)
        {
            price = 500;
        }
        else if (PassiveLevel == 2)
        {
            price = 600;
        }
        else if (PassiveLevel == 3)
        {
            price = 700;
        }
        else if (PassiveLevel == 4)
        {
            price = 800;
        }

        if (currentCoin < price) return;

        currentCoin -= price;
        audioSource.clip = buySound;
        audioSource.Play();
        PassiveLevel++;
        PassiveBuf += 0.1f;
        HealthManager.Instance.damagePlayer *= PassiveBuf;
        HealthManager.Instance.defendPlayer *= PassiveBuf;
        UpdateCoin();

        if (PassiveLevel == 5)
        {
            rightButton3TMP.text = "Passive \nlv.Max";
        }
        else
        {
            int nextPrice = 400 + (PassiveLevel * 100);
            rightButton3TMP.text = $"Passive lv.{PassiveLevel} \n{nextPrice}$";
        }

        allLevel += PassiveLevel;
        HealthManager.Instance.damagePlayer /= 1.1f;
        HealthManager.Instance.defendPlayer /= 1.1f;
    }
    public void buyHealth()
    {
        price = 1000;
        int health = 20;
        if (currentCoin < price) return;
        if (HealthManager.Instance.healthPlayer < HealthManager.Instance.maxHealthPlayer)
        {
            if (HealthManager.Instance.healthPlayer + health > HealthManager.Instance.maxHealthPlayer)
            {
                HealthManager.Instance.healthPlayer = HealthManager.Instance.maxHealthPlayer;
            }
            else
            {
                HealthManager.Instance.healthPlayer += health;
            }
            currentCoin -= price;
            audioSource.clip = reviveSound;
            audioSource.Play();
        }
        else
        {
            return;
        }
    }
    public void UpgradeCoin()
    {
        if (CoinLevel == 5) return;

        if (CoinLevel == 0)
        {
            price = 400;
        }
        else if (CoinLevel == 1)
        {
            price = 500;
        }
        else if (CoinLevel == 2)
        {
            price = 600;
        }
        else if (CoinLevel == 3)
        {
            price = 700;
        }
        else if (CoinLevel == 4)
        {
            price = 800;
        }

        if (currentCoin < price) return;

        currentCoin -= price;
        audioSource.clip = buySound;
        audioSource.Play();
        CoinLevel++;
        CoinBonus += 0.2f;
        UpdateCoin();

        if (CoinLevel == 5)
        {
            rightButton5TMP.text = "Coin \nlv.Max";
        }
        else
        {
            int nextPrice = 400 + (CoinLevel * 100);
            rightButton5TMP.text = $"Coin lv.{CoinLevel} \n{nextPrice}$";
        }
    }
}