using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingManager : MonoBehaviour
{
    public TextMeshProUGUI tmpSound;

    private bool isTrue = true;

    public void BackToStates()
    {
        SceneManager.LoadScene("States");
    }

    public void BackToHomePage()
    {
        SceneManager.LoadScene("HomePage");
    }

    public void SoundOnOff()
    {
        if (isTrue)
        {
            tmpSound.SetText("Sound OFF");
            isTrue = !isTrue;
        }
        else
        {
            tmpSound.SetText("Sound ON");
            isTrue = !isTrue;
        }

    }public void MusicOnOff()
    {
        if (isTrue)
        {
            tmpSound.SetText("Music OFF");
            BackgroundMusic.Instance.StopBackgroundMusic();
            isTrue = !isTrue;
        }
        else
        {
            tmpSound.SetText("Music ON");
            BackgroundMusic.Instance.PlayBackgroundMusic();
            isTrue = !isTrue;
        }

    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
