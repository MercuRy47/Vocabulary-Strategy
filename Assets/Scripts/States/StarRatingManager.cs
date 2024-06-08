using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarRatingManager : MonoBehaviour
{
    [Header("Star Rating Game Objects")]
    public GameObject[] starRatingObjects = new GameObject[7];

    private static int[] starCounts = new int[7];

    private void Start()
    {
        LoadStarCounts();
        UpdateStarRatings(true);
    }

    private void Update()
    {
        // อัพเดทค่าในทุกเฟรม (ถ้าต้องการ)
        UpdateStarRatings();
    }

    private void UpdateStarRatings(bool isLoading = false)
    {
        for (int i = 0; i < starRatingObjects.Length; i++)
        {
            if (starRatingObjects[i] != null)
            {
                StarRating starRating = starRatingObjects[i].GetComponent<StarRating>();
                if (starRating != null)
                {
                    if (isLoading)
                    {
                        // เมื่อโหลดค่า ให้เซ็ตค่า count ตามที่บันทึกไว้
                        starRating.count = starCounts[i];
                    }
                    else
                    {
                        // บันทึกค่า count ลงใน starCounts
                        starCounts[i] = starRating.count;
                    }
                }
            }
        }
        if (!isLoading)
        {
            SaveStarCounts();
        }
        LogStarCounts();
    }

    private void LogStarCounts()
    {
        for (int i = 0; i < starCounts.Length; i++)
        {
            Debug.Log($"StarRating {i + 1}: {starCounts[i]}");
        }
    }

    public static int GetStarCount(int index)
    {
        if (index >= 0 && index < starCounts.Length)
        {
            return starCounts[index];
        }
        return -1;
    }

    private void SaveStarCounts()
    {
        for (int i = 0; i < starCounts.Length; i++)
        {
            PlayerPrefs.SetInt($"StarCount_{i}", starCounts[i]);
        }
        PlayerPrefs.Save();
    }

    private void LoadStarCounts()
    {
        for (int i = 0; i < starCounts.Length; i++)
        {
            starCounts[i] = PlayerPrefs.GetInt($"StarCount_{i}", 0);
        }
    }
}
