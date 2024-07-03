using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInfoEnemy : MonoBehaviour
{
    // อ้างอิงถึง Object ที่จะแสดงหรือซ่อน
    public GameObject targetObject;

    void Start()
    {
        // ซ่อน Object ในตอนเริ่มต้น
        if (targetObject != null)
        {
            targetObject.SetActive(false);
        }
    }

    // ฟังก์ชันนี้จะถูกเรียกเมื่อเมาส์ชี้ที่ Collider ของ Object นี้
    void OnMouseEnter()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(true);
        }
    }

    // ฟังก์ชันนี้จะถูกเรียกเมื่อเมาส์ออกจาก Collider ของ Object นี้
    void OnMouseExit()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(false);
        }
    }
}
