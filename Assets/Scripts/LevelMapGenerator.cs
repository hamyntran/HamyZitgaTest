using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelMapGenerator : MonoBehaviour
{
    [SerializeField] private LevelOptionRow levelRowPrefab;
    [SerializeField] ScrollRect _scrollRect;
    [SerializeField] private Transform contentHolder;
    
    public Dictionary<int, LevelOption> buttonByLevel = new Dictionary<int, LevelOption>();
    [SerializeField] private int level;
    
  public  const int COLUMN = 4;
  public int TotalLevel => level;

    private void Start()
    {
        GenerateLevelMap();
    }

    private void GenerateLevelMap()
    {
        int row = Mathf.CeilToInt(level / (float)COLUMN);

        for (int y = 0; y < row; y++)
        {
            if (y % 2 == 0)
            {
                Instantiate(levelRowPrefab, contentHolder).Init(y,true, TotalLevel);
            }
            else
            {
                Instantiate(levelRowPrefab, contentHolder).Init(y,false, TotalLevel);
            }
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