using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnhancedUI.EnhancedScroller;
using EnhancedUI;


public class LevelMapGenerator : MonoBehaviour, IEnhancedScrollerDelegate
{
    [SerializeField] private LevelOptionRow levelRowPrefab;
    public EnhancedScroller scroller;
    public static Dictionary<int, LevelOption> buttonByLevel = new ();
    private SmallList<Data> _data;
    
    public const int COLUMN = 4;
    public int TotalLevel => LevelMapManager.Instance.totalLevel;


    private void Start()
    {
        scroller.Delegate = this;
        GenerateLevelMap();
    }

    private void GenerateLevelMap()
    {
        _data = new SmallList<Data>();
        int row = Mathf.CeilToInt(TotalLevel / (float)COLUMN);

        for (int y = 0; y < row; y++)
        {
            _data.Insert(new Data() { row = y, max = TotalLevel, clockwise = y % 2 == 0 }, 0);
        }

        scroller.ReloadData();
        scroller.ScrollPosition =
            scroller.GetScrollPositionForDataIndex(_data.Count - 1, EnhancedScroller.CellViewPositionEnum.Before);
        
    }

    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        return Mathf.CeilToInt(TotalLevel / (float)COLUMN);
    }

    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return (dataIndex % 2 == 0 ? 30f : 100f);
    }

    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
     
        LevelOptionRow cellView = scroller.GetCellView(levelRowPrefab) as LevelOptionRow;
    
        cellView.name = "Cell " + dataIndex.ToString();

        cellView.SetData(_data[dataIndex]);

        return cellView;
    }
}