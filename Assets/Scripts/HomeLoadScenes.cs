using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeLoadScenes : MonoBehaviour
{
    public void GotoStates()
    {
        SceneManager.LoadScene("States");
    }

    public void GotoCredit()
    {
        SceneManager.LoadScene("EndCredit");
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
