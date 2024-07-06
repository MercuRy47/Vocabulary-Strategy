using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInfoStates : MonoBehaviour
{
    public GameObject targetObject;

    void Start()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(false);
        }
    }

    void OnMouseEnter()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(true);
        }
    }

    void OnMouseExit()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(false);
        }
    }
}
