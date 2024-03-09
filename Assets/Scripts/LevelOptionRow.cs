using UnityEngine;
using UnityEngine.UI;
using EnhancedUI.EnhancedScroller;

public class LevelOptionRow : EnhancedScrollerCellView
{
    [SerializeField] private LevelOption[] levelBtns;
    [SerializeField] private HorizontalLayoutGroup horizontalLayout;
    [SerializeField] private RectTransform vertLine;


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
                levelBtns[i].gameObject.SetActive(true);
                levelBtns[i].SetLevel(level);
            }

            bool locking = level > LevelMapManager.Instance.unlockedLevel;
            levelBtns[i].SetLockStatus(locking);
            if (!locking)
            {
                levelBtns[i].SetStarStatus(level);
            }
        }
    }

    public void SetData(Data data)
    {
        SetLevels(data.row, data.max);

        if (!data.clockwise)
        {
            horizontalLayout.reverseArrangement = true;
            horizontalLayout.childAlignment = TextAnchor.UpperRight;

            if (vertLine.anchoredPosition.x < 0)
            {
                vertLine.anchoredPosition *= new Vector2(-1, 1);
            }
        }
        else
        {
            horizontalLayout.reverseArrangement = false;
            horizontalLayout.childAlignment = TextAnchor.UpperLeft;

            if (vertLine.anchoredPosition.x > 0)
            {
                vertLine.anchoredPosition *= new Vector2(-1, 1);
            }
        }

        vertLine.gameObject.SetActive(data.row != 0);
    }
}

public class Data
{
    public int row;
    public int max;
    public bool clockwise;
}