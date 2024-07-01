using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class StarRating : MonoBehaviour
{
    [SerializeField] public int count;
    public Image starOne;
    public Image starTwo;
    public Image starThree;
    public Sprite starOn;
    public Sprite starOff;

    private void Update()
    {
        UpdateStars();
    }

    private void UpdateStars()
    {
        if (count >= 3)
        {
            starOne.sprite = starOn;
            starTwo.sprite = starOn;
            starThree.sprite = starOn;
        }
        else if (count >= 2)
        {
            starOne.sprite = starOn;
            starTwo.sprite = starOn;
            starThree.sprite = starOff;
        }
        else if (count >= 1)
        {
            starOne.sprite = starOn;
            starTwo.sprite = starOff;
            starThree.sprite = starOff;
        }
        else
        {
            starOne.sprite = starOff;
            starTwo.sprite = starOff;
            starThree.sprite = starOff;
        }
    }
}
