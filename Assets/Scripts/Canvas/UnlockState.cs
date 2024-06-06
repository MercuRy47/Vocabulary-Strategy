using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockState : MonoBehaviour
{
    public GameObject targetObject;
    public Image targetImage; // Image component ที่ต้องการเปลี่ยนรูป
    public Sprite unlockState; // รูปแรกเดิม
    public Sprite lockState; // รูปที่รับเข้ามา

    private bool unlocked = false;

    void Update()
    {
        if (targetObject != null && targetObject.name == "State - 1" && LoadScenes.countState >= 1)
        {
            unlocked = true;
        }
        else if (targetObject != null && targetObject.name == "State - 2" && LoadScenes.countState >= 2)
        {
            unlocked = true;
        }
        else if (targetObject != null && targetObject.name == "State - 3" && LoadScenes.countState >= 3)
        {
            unlocked = true;
        }
        else if (targetObject != null && targetObject.name == "State - 4" && LoadScenes.countState >= 4)
        {
            unlocked = true;
        }
        else if (targetObject != null && targetObject.name == "State - 5" && LoadScenes.countState >= 5)
        {
            unlocked = true;
        }
        else if (targetObject != null && targetObject.name == "State - 6" && LoadScenes.countState >= 6)
        {
            unlocked = true;
        }
        else if (targetObject != null && targetObject.name == "State - 7" && LoadScenes.countState >= 7)
        {
            unlocked = true;
        }
        else
        {
            unlocked = false;
        }
        /**
        Debug.Log("Unlocked state: " + unlocked);
        Debug.Log("Count state: " + LoadScenes.countState);
        Debug.Log("Name state: " + targetObject.name);
        **/

        Unlocked();
    }

    // Method เพื่อเปลี่ยนสถานะของ unlockState
    public void ToggleUnlockState()
    {
        unlocked = !lockState;
    }

    public void Unlocked()
    {
        if (unlocked)
        {
            targetImage.sprite = unlockState;
        }
        else
        {
            targetImage.sprite = lockState;
        }
    }
}
