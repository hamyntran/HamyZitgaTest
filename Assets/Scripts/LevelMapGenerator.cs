using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LevelMapGenerator : MonoBehaviour
{
    [SerializeField] private LevelBtn _levelBtnPrefab, _hLinePrefab, _vLinePrefab;
    [SerializeField] RectTransform _btnHolder;
    [SerializeField] private ScrollView _scrollView;

    [SerializeField] private List<LevelBtn> _allBtns;
    private LevelBtn[,] _btnArr;
    
    [SerializeField] private int level;
    
    [SerializeField] private int column;
    [SerializeField] private float columnSize, rowSize;
    [SerializeField] private float paddingDown, paddingLeft;
    [SerializeField] private float xSpace, ySpace;
    

    private void Start()
    {
        GenerateLevelMap();
    }

    private void GenerateLevelMap()
    {
        int row = Mathf.CeilToInt(level / column);

        int y = 0;
        int i = _allBtns.Count;
        
        _btnArr = new LevelBtn[column, row+1];
        Debug.Log($"C: {column}, R: {row}");

        for (; y <= row ; y++)
        {
            if (y % 2 == 0)
            {
                int x = (y == 0)? i:0;
                for (; x < column && i< level; x++)
                {
                    GenerateLevelButton(x, y);
                }
            }
            else
            {
                for (int x = column-1; x >=0 && i< level; x--)
                {
                    GenerateLevelButton(x, y);
                }
            }
        }

        void GenerateLevelButton(int iX, int iY)
        {
            var levelBtn =   Instantiate(_levelBtnPrefab, _btnHolder);
        //    levelBtn.rectTransform.anchoredPosition = GetRectPos(iX, iY);
          //  levelBtn.rectTransform.sizeDelta = new Vector2(columnSize,rowSize);

            _btnArr[iX, iY] = levelBtn;

            i++;
            levelBtn.SetLevel(i);
            _allBtns.Add(levelBtn);
        }
    }

    private Vector2 GetRectPos(int x, int y)
    {
        return new Vector2(paddingLeft +x * (columnSize+xSpace), paddingDown +y * (rowSize+ySpace) );
    }

    /*private void Update()
    {
        if (_btnArr.Length > 0)
        {
            for (int i = 0; i < _btnArr.GetLength(0); i++)
            {
                for (int j = 0; j < _btnArr.GetLength(1); j++)
                {
                    if (_btnArr[i, j] != null)
                    {
                        _btnArr[i,j].rectTransform.anchoredPosition = GetRectPos(i, j);
                    }
                }
            }
        }
    }*/
}
