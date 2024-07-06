using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGame : MonoBehaviour
{
    private void Awake()
    {
        if (PlayerPrefs.HasKey("CountState"))
        {
            LoadScenes.countState = PlayerPrefs.GetInt("CountState");
        }
        else
        {
            LoadScenes.countState = 1;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            LoadScenes.countState++;
            SaveCountState();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadScenes.countState = 1;
            SaveCountState();
        }
    }

    public static void SaveCountState()
    {
        PlayerPrefs.SetInt("CountState", LoadScenes.countState);
        PlayerPrefs.Save();
    }
}
