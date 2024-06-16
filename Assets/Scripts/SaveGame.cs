using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGame : MonoBehaviour
{
    private void Awake()
    {
        // โหลดค่าตัวแปร countState จาก PlayerPrefs
        if (PlayerPrefs.HasKey("CountState"))
        {
            LoadScenes.countState = PlayerPrefs.GetInt("CountState");
        }
        else
        {
            LoadScenes.countState = 0; // ตั้งค่าเริ่มต้นหากไม่มีค่าใน PlayerPrefs
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            LoadScenes.countState++;
            SaveCountState(); // บันทึกค่าตัวแปร countState ทุกครั้งที่มีการเปลี่ยนแปลง
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadScenes.countState = 0;
            SaveCountState();
        }
    }

    public static void SaveCountState()
    {
        PlayerPrefs.SetInt("CountState", LoadScenes.countState);
        PlayerPrefs.Save();
    }
}
