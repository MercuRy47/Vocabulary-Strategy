using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button), typeof(AudioSource))]
public class StateButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float hoverScale = 1.1f; // ��Ҵ�������Т��¢ͧ�����ⴹ�����
    public AudioClip hoverSound; // ���§�����������ⴹ�����
    public AudioClip clickSound; // ���§����������ͤ�ԡ

    private Button button;
    private AudioSource audioSource;
    private Vector3 originalScale;

    void Start()
    {
        button = GetComponent<Button>();
        audioSource = GetComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.Stop();

        // �红�Ҵ����ͧ����
        originalScale = transform.localScale;

        // ���� Listener ����Ѻ����
        button.onClick.AddListener(OnClick);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // �����ⴹ�����
        if (button.interactable)
        {
            transform.localScale = originalScale * hoverScale; // ���¢�Ҵ
            audioSource.clip = hoverSound; // ��˹����§
            audioSource.Play(); // ������§
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // ��������ⴹ�����
        transform.localScale = originalScale; // �׹��Ҵ���
    }

    void OnClick()
    {
        if (button.interactable)
        {
            audioSource.clip = clickSound;
            audioSource.Play();
        }
    }
}
