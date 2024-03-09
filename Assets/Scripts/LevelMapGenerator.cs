using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnhancedUI.EnhancedScroller;
using EnhancedUI;


public class LevelMapGenerator : MonoBehaviour, IEnhancedScrollerDelegate
{
    [SerializeField] private LevelOptionRow levelRowPrefab;
    public EnhancedScroller scroller;
    public Dictionary<int, LevelOption> buttonByLevel = new Dictionary<int, LevelOption>();
    public int TotalLevel => LevelMapManager.Instance.totalLevel;
    private SmallList<Data> _data;
    public const int COLUMN = 4;

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
        // StartCoroutine(JumpToLastAtEndOfFrame());

        IEnumerator JumpToLastAtEndOfFrame()
        {
            yield return null;
            scroller.JumpToDataIndex(_data.Count - 1);
        }
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
        // first, we get a cell from the scroller by passing a prefab.
        // if the scroller finds one it can recycle it will do so, otherwise
        // it will create a new cell.
        LevelOptionRow cellView = scroller.GetCellView(levelRowPrefab) as LevelOptionRow;

        // set the name of the game object to the cell's data index.
        // this is optional, but it helps up debug the objects in 
        // the scene hierarchy.
        cellView.name = "Cell " + dataIndex.ToString();

        // in this example, we just pass the data to our cell's view which will update its UI
        cellView.SetData(_data[dataIndex]);

        // return the cell to the scroller
        return cellView;
    }
}