using System;
using TMPro;
using UnityEngine;

public class LevelOption : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelStr;
    [SerializeField] private GameObject _lockIMG;

    public void SetLevel(int lvl)
    {
        _levelStr.text = lvl.ToString();
    }

    private void SetLockStatus(bool status)
    {
        _lockIMG.SetActive(status);
    }
}
