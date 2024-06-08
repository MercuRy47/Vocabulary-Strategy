using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    [Header("Count State")]
    public static int countState;
    private int count;

    private void Start()
    {
        count = countState;
    }

    public void StateTutorial()
    {
        if (countState >= 0)
        {
            SceneManager.LoadScene("Tutorial");
        }
    }

    public void StateOne()
    {
        if (countState >= 1)
        {  
            SceneManager.LoadScene("State-1");
        }
    }

    public void StateTwo()
    {
        if (countState >= 2)
        {
            SceneManager.LoadScene("State-2");
        }
    }

    public void StateThree()
    {
        if (countState >= 3)
        {
            SceneManager.LoadScene("State-3");
        }
    }

    public void StateFour()
    {
        if (countState >= 4)
        {
            SceneManager.LoadScene("State-4");
        }
    }

    public void StateFive()
    {
        if (countState >= 5)
        {
            SceneManager.LoadScene("State-5");
        }
    }

    public void StateSix()
    {
        if (countState >= 6)
        {
            SceneManager.LoadScene("State-6");
        }
    }

    public void StateSeven()
    {
        if (countState >= 7)
        {
            SceneManager.LoadScene("State-7");
        }
    }
}
