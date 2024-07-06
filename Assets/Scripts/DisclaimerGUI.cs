using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisclaimerGUI : MonoBehaviour
{
    public GameObject disclaimerGUI;
    private bool isTrue;
    public void ShowDisclaimer()
    {
        isTrue = !isTrue;
        disclaimerGUI.SetActive(isTrue);
    }
}
