using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelOptionRow : MonoBehaviour
{
    [SerializeField] private LevelOption[] levelBtns;
    [SerializeField] private HorizontalLayoutGroup horizontalLayout;
    [SerializeField] private RectTransform vertLine;
    private bool clockWise;

    public void Init(int row, bool clockwise, int max)
    {
        clockWise = clockwise;

      SetLevels(row, max);
      
      if (!clockwise)
      {
          horizontalLayout.reverseArrangement = true;
          horizontalLayout.childAlignment = TextAnchor.UpperRight;
          vertLine.anchoredPosition *= new Vector2(-1, 1);
      }
      
      if ((max - 1) / LevelMapGenerator.COLUMN <= row)
      {
          vertLine.gameObject.SetActive(false);
      }
    }


    private void SetLevels(int row, int max)
    {
        for (int i = 0; i < LevelMapGenerator.COLUMN; i++)
        {
            int level = row * LevelMapGenerator.COLUMN + i + 1;

            if (level > max)
            {
                levelBtns[i].gameObject.SetActive(false);
            }
            else
            {
                levelBtns[i].SetLevel(level);
            }
        }
    }
    
    private void SetLevelsAntiClockwise(int row, int max)
    {
        for (int i = 0; i < LevelMapGenerator.COLUMN; i++)
        {
            int level = row * LevelMapGenerator.COLUMN + i + 1;

            if (level > max)
            {
                levelBtns[i].gameObject.SetActive(false);
            }
            else
            {
                levelBtns[ LevelMapGenerator.COLUMN -1-i].SetLevel(level);
            }
        }
    }
}
