using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitEndCredit : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackToHomePage();
        }
    }

    public void BackToHomePage()
    {
        SceneManager.LoadScene("HomePage");
    }
}
