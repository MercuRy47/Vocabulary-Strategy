using System.Collections;
using UnityEngine;

public class ScaleSprite : MonoBehaviour
{
    public static ScaleSprite Instance { get; private set; }

    public Transform spriteTransform; // อ้างอิงไปยัง Transform ของ Sprite ที่ต้องการขยาย
    public float targetScale = 3.5f; // ขนาดเป้าหมาย
    public float duration = 2f; // ระยะเวลาในการขยาย

    public static bool shieldOn = false;

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
        // เริ่ม Coroutine ในการขยาย scale
        //UseShield();
    }

    public void UseShield()
    {
        StartCoroutine(ScaleOverTime(spriteTransform, targetScale, duration));
    }

    public void ResetScale()
    {
        spriteTransform.localScale = new Vector3(1, 1, spriteTransform.localScale.z);
        shieldOn = false;
    }

    private IEnumerator ScaleOverTime(Transform target, float targetScale, float duration)
    {
        Vector3 initialScale = target.localScale;
        Vector3 finalScale = new Vector3(targetScale, targetScale, target.localScale.z);
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            target.localScale = Vector3.Lerp(initialScale, finalScale, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        target.localScale = finalScale; // ให้แน่ใจว่าได้ขนาดสุดท้ายเป๊ะๆ
        shieldOn = true;
    }
}
