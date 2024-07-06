using UnityEngine;
using System.Collections;

public class MoveAndAnimate : MonoBehaviour
{
    public Transform target; // �ش���»��·ҧ��� x2
    public float speed = 1f; // ��������㹡�â�Ѻ
    public Animator animator; // ��ҧ�ԧ��ѧ Animator �ͧ GameObject
    public AnimationClip AttackAnimation; // ��ҧ�ԧ��ѧ Animation Clip �á�������
    public AnimationClip IdleAnimation; // ��ҧ�ԧ��ѧ Animation Clip ����ͧ���������ѧ�ҡ Animation �á

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool isMoving = false; // ʶҹС������͹���

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
        isMoving = true; // ��˹�ʶҹС������͹����繨�ԧ

        // ������Ѻ�ҡ x1 � x2
        audioSource.clip = dashSound;
        audioSource.Play();
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

        // ��� animation �á 2 ������ѧ�ҡ����ش x2
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
                if (ScaleSprite.shieldOn) damage = HealthManager.Instance.damageEnemy * 0.1f;
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

        // ��� animation ����ͧ��ѧ�ҡ animation �á�ú 2 ����
        animator.Play(IdleAnimation.name);
        yield return new WaitForSeconds(IdleAnimation.length);

        // ������Ѻ�ҡ x2 ��Ѻ� x1
        audioSource.clip = dashSound;
        audioSource.Play();
        while (Vector3.Distance(transform.position, startPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
            yield return null;
        }

        isMoving = false; // ��˹�ʶҹС������͹�������
    }
}
