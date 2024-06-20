using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic Instance { get; private set; }

    [Header("Sound")]
    public AudioClip backgroundSound;

    private AudioSource audioSource;

    private void Awake()
    {
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
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.Stop();
    }

    public void PlayBackgroundMusic()
    {
        audioSource.clip = backgroundSound;
        audioSource.Play();
    }

    public void StopBackgroundMusic()
    {
        audioSource.clip = backgroundSound;
        audioSource.Stop();
    }
}
