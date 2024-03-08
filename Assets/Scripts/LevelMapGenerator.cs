using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMapGenerator : MonoBehaviour
{
    [SerializeField] private LevelOption levelOptionPrefab;
    [SerializeField] ScrollRect _scrollRect;
    [SerializeField] private Transform contentHolder;
    [SerializeField] private GridLayoutGroup gridGroup;

    [SerializeField] private List<LevelOption> _allBtns;
    [SerializeField] private Dictionary<int, LevelOption> _buttonByLevel = new Dictionary<int, LevelOption>();
    [SerializeField] private int level;

    private void Start()
    {
        GenerateLevelMap();
    }

    private void GenerateLevelMap()
    {
        int column = (int)((GetViewBoundsExtent().x * 2 - gridGroup.padding.left - gridGroup.padding.right) /
                           gridGroup.cellSize.x);
        int row = Mathf.CeilToInt(level / (float)column);

        int i = _allBtns.Count + 1;

        for (int y = 0; y < row; y++)
        {
            if (y % 2 == 0)
            {
                int x = (y == 0) ? i - 1 : 0;

                for (; x < column && i <= level; x++)
                {
                    GenerateLevelBtn(i);
                    i++;
                }
            }
            else
            {
                for (int x = 0; x < column; x++)
                {
                    int no = (y + 1) * column - x;
                    
                    if (no > level)
                    {
                        new GameObject().AddComponent<RectTransform>().transform.parent = contentHolder;
                    }
                    else
                    {
                        GenerateLevelBtn(no);
                    }

                    i++;
                }
            }
        }

        void GenerateLevelBtn(int levelNo)
        {
            var newBtn = Instantiate(levelOptionPrefab, contentHolder);
            newBtn.SetLevel(levelNo);
        }
    }


    private Vector2 GetViewBoundsExtent()
    {
        RectTransform viewRect = _scrollRect.viewport;
        RectTransform contentRect = _scrollRect.content;

        Vector3[] viewCorners = new Vector3[4];
        viewRect.GetWorldCorners(viewCorners);

        Vector3[] contentCorners = new Vector3[4];
        contentRect.GetWorldCorners(contentCorners);

        Matrix4x4 viewToLocalMatrix = contentRect.worldToLocalMatrix * viewRect.localToWorldMatrix;

        Vector3[] localViewCorners = new Vector3[4];
        for (int i = 0; i < 4; i++)
        {
            localViewCorners[i] = viewToLocalMatrix.MultiplyPoint(viewCorners[i]);
        }

        float extentX = Mathf.Abs(localViewCorners[2].x - localViewCorners[0].x) * 0.5f;
        float extentY = Mathf.Abs(localViewCorners[2].y - localViewCorners[0].y) * 0.5f;

        return new Vector2(extentX, extentY);
    }
}