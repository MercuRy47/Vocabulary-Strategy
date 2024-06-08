using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHide : MonoBehaviour
{
    [Header("Setting")]
    public GameObject settingGUI;

    [Header("Questions")]
    public GameObject questionBarGUI;

    private bool isTrue;

    private void Start()
    {
        settingGUI.SetActive(false);
        questionBarGUI.SetActive(false);
    }

    public void SettingGUI()
    {
        isTrue = !isTrue;
        settingGUI.SetActive(isTrue);
    }

    public void QuestionBarGUI()
    {
        isTrue = !isTrue;
        questionBarGUI.SetActive(isTrue);
    }
}
