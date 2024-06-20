using UnityEngine;
using System.Collections;

public class MoveAndAnimate : MonoBehaviour
{
    public Transform target; // จุดหมายปลายทางที่ x2
    public float speed = 1f; // ความเร็วในการขยับ
    public Animator animator; // อ้างอิงไปยัง Animator ของ GameObject
    public AnimationClip AttackAnimation; // อ้างอิงไปยัง Animation Clip แรกที่จะเล่น
    public AnimationClip IdleAnimation; // อ้างอิงไปยัง Animation Clip ที่สองที่จะเล่นหลังจาก Animation แรก

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool isMoving = false; // สถานะการเคลื่อนที่

    public static float damage;

    [Header("Sound")]
    public AudioClip attackSound;
    //public AudioClip defendSound;
    public AudioClip dashSound;

    private AudioSource audioSource;

    void Start()
    {
        startPosition = transform.position;
        if (target != null)
        {
            targetPosition = target.position;
        }

        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.Stop();
    }

    void Update()
    {
        ///
    }

    public void StartMoving()
    {
        StartCoroutine(MoveAndPlayAnimation());
    }

    IEnumerator MoveAndPlayAnimation()
    {
        isMoving = true; // กำหนดสถานะการเคลื่อนที่เป็นจริง

        // ค่อยๆขยับจาก x1 ไป x2
        audioSource.clip = dashSound;
        audioSource.Play();
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

        // เล่น animation แรก 2 ครั้งหลังจากอยู่จุด x2
        for (int i = 0; i < 2; i++)
        {
            animator.Play(AttackAnimation.name);
            yield return new WaitForSeconds(AttackAnimation.length);
            if (gameObject.name == "Torch_Red")
            {
                audioSource.clip = attackSound;
                audioSource.Play();
                damage = HealthManager.Instance.damageEnemy - HealthManager.Instance.defendPlayer;
                if (damage <= 0) damage = HealthManager.Instance.damageEnemy * 0.3f;
                if (damage <= HealthManager.Instance.damageEnemy / 2) damage = HealthManager.Instance.damageEnemy * 0.5f;
                if (ScaleSprite.shieldOn) damage *= 0.1f;
                damage = damage / 2;
                HealthManager.Instance.AttackPlayer(damage);
            }
            else if (gameObject.name == "Knight_Blue")
            {
                audioSource.clip = attackSound;
                audioSource.Play();
                damage = HealthManager.Instance.damagePlayer - HealthManager.Instance.defendEnemy;
                if (damage <= 0) damage = HealthManager.Instance.damagePlayer * 0.3f;
                if (damage <= HealthManager.Instance.damagePlayer / 2) damage = HealthManager.Instance.damagePlayer * 0.5f;
                damage = damage / 2;
                HealthManager.Instance.AttackEnemy(damage);
            }
        }

        // เล่น animation ที่สองหลังจาก animation แรกครบ 2 ครั้ง
        animator.Play(IdleAnimation.name);
        yield return new WaitForSeconds(IdleAnimation.length);

        // ค่อยๆขยับจาก x2 กลับไป x1
        audioSource.clip = dashSound;
        audioSource.Play();
        while (Vector3.Distance(transform.position, startPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
            yield return null;
        }

        isMoving = false; // กำหนดสถานะการเคลื่อนที่เป็นเท็จ
    }
}
