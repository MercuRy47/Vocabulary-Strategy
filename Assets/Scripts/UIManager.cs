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

    private int price;
    private static float currentCoin;
    private int AtkLevel = 0;
    private int DefLevel = 0;
    private int PassiveLevel = 0;
    private float PassiveBuf = 1;
    private int CoinLevel = 0;
    private float CoinBonus = 1f;

    private void Start()
    {
        currentCoin = 100000;
        UpdateCoin();
        StartRightUI();
    }

    private void Update()
    {
        UpdateCoin();
        UpdateStats();
    }

    public void UpdateCoin()
    {
        coinTMP.text = $"{currentCoin}$";
    }

    public void UpdateStats()
    {
        statsTMP.text = $"Correct: {CheckAnswer.correctScore} \nWrong: {CheckAnswer.wrongScore} \nAttack: {(int)HealthManager.Instance.damagePlayer} \nDefend: {(int)HealthManager.Instance.defendPlayer} \nPassive: >W< \nCoin Bonus: x{CoinBonus}";
    }

    public void StartRightUI()
    {
        rightButton1TMP.text = $"Atk. lv.{AtkLevel} \n{300}$";
        rightButton2TMP.text = $"Def. lv.{DefLevel} \n{300}$";
        rightButton3TMP.text = $"Passive lv.{PassiveLevel} \n{300}$";
        rightButton4TMP.text = $"+{20} HP \n{300}$";
        rightButton5TMP.text = $"Coin lv.{CoinLevel} \n{300}$";
    }

    public void buyAttack()
    {
        if (AtkLevel == 5) return;

        if (AtkLevel == 0)
        {
            price = 300;
        }
        else if (AtkLevel == 1)
        {
            price = 600;
        }
        else if (AtkLevel == 2)
        {
            price = 1200;
        }
        else if (AtkLevel == 3)
        {
            price = 2400;
        }
        else if (AtkLevel == 4)
        {
            price = 4800;
        }

        if (currentCoin < price) return;

        currentCoin -= price;
        AtkLevel++;
        HealthManager.Instance.damagePlayer += 2;
        UpdateCoin();

        if (AtkLevel == 5)
        {
            rightButton1TMP.text = "Atk. \nlv.Max";
        }
        else
        {
            int nextPrice = 300 * (int)Mathf.Pow(2, AtkLevel);
            rightButton1TMP.text = $"Atk. lv.{AtkLevel} \n{nextPrice}$";
        }
    }
    public void buyDefend()
    {
        if (DefLevel == 5) return;

        if (DefLevel == 0)
        {
            price = 300;
        }
        else if (DefLevel == 1)
        {
            price = 600;
        }
        else if (DefLevel == 2)
        {
            price = 1200;
        }
        else if (DefLevel == 3)
        {
            price = 2400;
        }
        else if (DefLevel == 4)
        {
            price = 4800;
        }

        if (currentCoin < price) return;

        currentCoin -= price;
        DefLevel++;
        HealthManager.Instance.defendPlayer += 2;
        UpdateCoin();

        if (DefLevel == 5)
        {
            rightButton2TMP.text = "Def. \nlv.Max";
        }
        else
        {
            int nextPrice = 300 * (int)Mathf.Pow(2, DefLevel);
            rightButton2TMP.text = $"Def. lv.{DefLevel} \n{nextPrice}$";
        }
    }
    public void buyPassive()
    {
        if (PassiveLevel == 5) return;

        if (PassiveLevel == 0)
        {
            price = 300;
        }
        else if (PassiveLevel == 1)
        {
            price = 600;
        }
        else if (PassiveLevel == 2)
        {
            price = 1200;
        }
        else if (PassiveLevel == 3)
        {
            price = 2400;
        }
        else if (PassiveLevel == 4)
        {
            price = 4800;
        }

        if (currentCoin < price) return;

        currentCoin -= price;
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
            int nextPrice = 300 * (int)Mathf.Pow(2, PassiveLevel);
            rightButton3TMP.text = $"Passive lv.{PassiveLevel} \n{nextPrice}$";
        }
    }
    public void buyHealth()
    {
        price = 300;
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
            price = 300;
        }
        else if (CoinLevel == 1)
        {
            price = 600;
        }
        else if (CoinLevel == 2)
        {
            price = 1200;
        }
        else if (CoinLevel == 3)
        {
            price = 2400;
        }
        else if (CoinLevel == 4)
        {
            price = 4800;
        }

        if (currentCoin < price) return;

        currentCoin -= price;
        CoinLevel++;
        CoinBonus += 0.2f;
        UpdateCoin();

        if (CoinLevel == 5)
        {
            rightButton5TMP.text = "Coin \nlv.Max";
        }
        else
        {
            int nextPrice = 300 * (int)Mathf.Pow(2, CoinLevel);
            rightButton5TMP.text = $"Coin lv.{CoinLevel} \n{nextPrice}$";
        }
    }
}