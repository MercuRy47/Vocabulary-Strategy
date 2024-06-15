using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingManager : MonoBehaviour
{
    public GameObject summarizeGUI;
    public GameObject allGUI;
    public GameObject fightingGUI;

    // อ้างอิงถึง GameObject ทั้งสอง
    public GameObject Player;
    public GameObject shieldPlayer;
    public GameObject Enemy;
    public GameObject shieldEnemy;

    private MoveAndAnimate script1;
    private MoveAndAnimate script2;

    public static int buttonCount;

    void Start()
    {
        buttonCount = -1;
        summarizeGUI.SetActive(false);
        // ดึงสคริปต์ MoveAndAnimate จาก GameObject ทั้งสอง
        script1 = Player.GetComponent<MoveAndAnimate>();
        script2 = Enemy.GetComponent<MoveAndAnimate>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetOrderInLayer(Player, 1);
            script1.StartMoving();
            SetOrderInLayer(Player, 0);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetOrderInLayer(Enemy, 1);
            script2.StartMoving();
            SetOrderInLayer(Enemy, 0);
        }

        if (HealthManager.Instance.healthEnemy <= 0)
        {
            allGUI.SetActive(false);
            fightingGUI.SetActive(false);
            summarizeGUI.SetActive(true);
        }
    }

    public void AttackPlayer()
    {
        if (buttonCount == 1) return;
        if (buttonCount <= 0)
        {
            StartCoroutine(AttackCooldown());
        }
    }

    IEnumerator AttackCooldown()
    {
        script1.StartMoving();
        buttonCount = 1;
        yield return new WaitForSeconds(3f); // รอ 3 วินาที
        script2.StartMoving();
    }

    public void DefendPlayer()
    {
        if (buttonCount == 1) return;
        if (buttonCount <= 0)
        {
            StartCoroutine(DefendCooldown());
        }      
    }

    IEnumerator DefendCooldown()
    {
        shieldPlayer.SetActive(true);
        ScaleSprite.Instance.UseShield();     
        buttonCount = 1;
        yield return new WaitForSeconds(2f);
        script2.StartMoving();
        yield return new WaitForSeconds(2f);
        shieldPlayer.SetActive(false);
        ScaleSprite.Instance.ResetScale();
    }

    private void SetOrderInLayer(GameObject obj, int order)
    {
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.sortingOrder = order;
        }
    }
}
