using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EnhancedUI.EnhancedScroller;
using EnhancedUI;
using UnityEngine.UIElements;


public class LevelMapGenerator : MonoBehaviour, IEnhancedScrollerDelegate
{
    [SerializeField] private LevelOptionRow levelRowPrefab;

    public EnhancedScroller scroller;
    public Dictionary<int, LevelOption> buttonByLevel = new Dictionary<int, LevelOption>();

    [SerializeField] private int level;
    public int unlocked_level;
    public int TotalLevel => level;

    private SmallList<Data> _data;

    public const int COLUMN = 4;
    public const int VIEWABLE_ROW = 10;

    private ScrollView _scrollView;

    private void Start()
    {
        //    GenerateLevelMap();
        scroller.Delegate = this;

        // load in a large set of data
        GenerateLevelMap();
    }

    /*
    private void LoadLargeData()
    {
        // set up some simple data
        _data = new SmallList<Data>();
        for (var i = 0; i < 1000; i++)
            _data.Add(new Data() {index = i});

        // tell the scroller to reload now that we have the data
        scroller.ReloadData();
    }
    */


    private void GenerateLevelMap()
    {
        _data = new SmallList<Data>();
        int row = Mathf.CeilToInt(level / (float)COLUMN);

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