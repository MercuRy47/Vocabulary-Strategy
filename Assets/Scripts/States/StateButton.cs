using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button), typeof(AudioSource))]
public class StateButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float hoverScale = 1.1f; // ขนาดที่ปุ่มจะขยายของเมื่อโดนเมาส์
    public AudioClip hoverSound; // เสียงที่เล่นเมื่อโดนเมาส์
    public AudioClip clickSound; // เสียงที่เล่นเมื่อคลิก

    private Button button;
    private AudioSource audioSource;
    private Vector3 originalScale;

    void Start()
    {
        button = GetComponent<Button>();
        audioSource = GetComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.Stop();

        // เก็บขนาดเดิมของปุ่ม
        originalScale = transform.localScale;

        // เพิ่ม Listener สำหรับปุ่ม
        button.onClick.AddListener(OnClick);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // เมื่อโดนเมาส์
        if (button.interactable)
        {
            transform.localScale = originalScale * hoverScale; // ขยายขนาด
            audioSource.clip = hoverSound; // กำหนดเสียง
            audioSource.Play(); // เล่นเสียง
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // เมื่อไม่โดนเมาส์
        transform.localScale = originalScale; // คืนขนาดเดิม
    }

    void OnClick()
    {
        // เมื่อคลิก
        if (button.interactable)
        {
            audioSource.clip = clickSound; // กำหนดเสียง
            audioSource.Play(); // เล่นเสียง
        }
    }
}
