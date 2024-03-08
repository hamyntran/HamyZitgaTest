using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelBtn : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelStr;
    public RectTransform rectTransform;

    public void SetLevel(int lvl)
    {
        _levelStr.text = lvl.ToString();
    }
}
