using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingManager : MonoBehaviour
{
    public GameObject winGUI;
    public GameObject loseGUI;
    public GameObject allGUI;
    public GameObject fightingGUI;

    // อ้างอิงถึง GameObject ทั้งสอง
    public GameObject Player;
    public GameObject shieldPlayer;
    public GameObject Enemy;
    public GameObject shieldEnemy;

    private MoveAndAnimate script1;
    private MoveAndAnimate script2;

    [Header("Buttons")]
    public GameObject optionsButtons;
    public GameObject continueButton;

    public static int buttonCount;

    void Start()
    {
        buttonCount = -1;
        winGUI.SetActive(false);
        loseGUI.SetActive(false);
        // ดึงสคริปต์ MoveAndAnimate จาก GameObject ทั้งสอง
        script1 = Player.GetComponent<MoveAndAnimate>();
        script2 = Enemy.GetComponent<MoveAndAnimate>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            script1.StartMoving();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            script2.StartMoving();
        }

        if (HealthManager.Instance.healthEnemy <= 0)
        {
            SummarizeManager.Instance.CalScore();
            allGUI.SetActive(false);
            fightingGUI.SetActive(false);
            winGUI.SetActive(true);
        }
        if (HealthManager.Instance.healthPlayer <= 0)
        {
            SummarizeManager.Instance.CalScore();
            allGUI.SetActive(false);
            fightingGUI.SetActive(false);
            loseGUI.SetActive(true);
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
        
        if (HealthManager.Instance.healthEnemy <= 0)
        {
            yield break; // หยุดการทำงานของ Coroutine ถ้าเงื่อนไขเป็นจริง
        }

        buttonCount = 1;
        yield return new WaitForSeconds(3f); // รอ 3 วินาที

        if (HealthManager.Instance.healthEnemy > 0) // ตรวจสอบอีกครั้งก่อนเริ่มเคลื่อนที่ script2
        {
            script2.StartMoving();
            yield return new WaitForSeconds(2.5f);
            optionsButtons.SetActive(false);
            continueButton.SetActive(true);
        }
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
        yield return new WaitForSeconds(0.5f);
        optionsButtons.SetActive(false);
        continueButton.SetActive(true);
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
