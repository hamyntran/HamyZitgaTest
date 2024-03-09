using System;
using TMPro;
using UnityEngine;

public class LevelOption : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelStr;
    [SerializeField] private GameObject _lockIMG, starHolder;
    [SerializeField] private GameObject[] starsIMG;

    public void SetLevel(int lvl)
    {
        _levelStr.text = lvl.ToString();
    }

    public void SetLockStatus(bool lockStatus)
    {
        _lockIMG.SetActive(lockStatus);
        starHolder.SetActive(!lockStatus);
    }

    public void SetStarStatus(int level)
    {
        int star =  PlayerPrefs.GetInt("Level star" +level);

        int i = 0;
        for (; i < star; i++)
        {
            starsIMG[i].SetActive(true);
        }
        
        for (; i < starsIMG.Length; i++)
        {
            starsIMG[i].SetActive(false);
        }
    }
}
