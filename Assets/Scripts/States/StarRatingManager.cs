using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarRatingManager : MonoBehaviour
{
    [Header("Star Rating Game Objects")]
    public GameObject[] starRatingObjects = new GameObject[7];

    public static int[] starCounts = new int[7];

    private void Start()
    {
        LoadStarCounts();
        UpdateStarRatings(true);
    }

    private void Update()
    {
        // �Ѿഷ���㹷ء��� (��ҵ�ͧ���)
        UpdateStarRatings();

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetStarCounts();
        }
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
                        // �������Ŵ��� ����絤�� count ������ѹ�֡���
                        starRating.count = starCounts[i];
                    }
                    else
                    {
                        // �ѹ�֡��� count ŧ� starCounts
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

    // �����ѧ��ѹ������������ starCounts
    public static void AddToStarCount(int index, int value)
    {
        if (index >= 0 && index < starCounts.Length)
        {
            starCounts[index] = value;
            PlayerPrefs.SetInt($"StarCount_{index}", starCounts[index]);
            PlayerPrefs.Save();
        }
    }

    private void ResetStarCounts()
    {
        for (int i = 0; i < starCounts.Length; i++)
        {
            starCounts[i] = 0;
            PlayerPrefs.SetInt($"StarCount_{i}", 0);
        }
        PlayerPrefs.Save();
        UpdateStarRatings(true);
        Debug.Log("Star counts have been reset.");
    }
}
