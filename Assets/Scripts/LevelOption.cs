using System;
using TMPro;
using UnityEngine;

public class LevelOption : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelStr;

    public void SetLevel(int lvl)
    {
        _levelStr.text = lvl.ToString();
    }

  
}
