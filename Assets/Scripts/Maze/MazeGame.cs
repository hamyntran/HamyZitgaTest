using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class MazeGame : MonoBehaviour
{
    [SerializeField] private MazeGenerator _mazeGenerator;
    [SerializeField] private Camera camera;
    [SerializeField] private Transform _bgIMG;
    [SerializeField] private BugMovement _bugPrefab;
    [SerializeField] private GameObject _targetPrefab;
    [SerializeField] private LineRenderer _hintLine;
    private BugMovement _bugMovement;

    private Vector2Int _startPos, _endPos;
    private List<Vector2Int> foundPath;

    public string levelSelectionScene;

    private void Start()
    {
        _hintLine.positionCount = 0;
       if(foundPath!=null)  foundPath.Clear();

        _mazeGenerator.Generate();
        GenerateBugAtStart();
        GenerateRandomTarget();
        SetCameraPosition();
    }

    private void SetCameraPosition()
    {
        camera.transform.position = new Vector3((float)(_mazeGenerator.Width - 1) / 2,
            (float)(_mazeGenerator.Height - 1) / 2, -10);
        _bgIMG.transform.position =
            new Vector3((float)(_mazeGenerator.Width - 1) / 2, (float)(_mazeGenerator.Height - 1) / 2, 0);
    }

    private void GenerateBugAtStart()
    {
        _bugMovement =
            Instantiate(_bugPrefab, new Vector3(0, _mazeGenerator.Height - 1, 0), Quaternion.identity) as BugMovement;
        _startPos = new Vector2Int(0, _mazeGenerator.Height - 1);
    }

    private void GenerateRandomTarget()
    {
        int randX = Random.Range(0, _mazeGenerator.Width);
        int randY = Random.Range(0, _mazeGenerator.Height);
        Instantiate(_targetPrefab, new Vector3(randX, randY, 0), Quaternion.identity);
        _endPos = new Vector2Int(randX, randY);
    }

    public void Hint()
    {
        Pathfinding pathfind = new Pathfinding(_mazeGenerator.RoomGrid);
        foundPath = pathfind.FindPath(_startPos, _endPos);

        if (foundPath != null &&foundPath.Count>0)
        {
            SetUpLine(foundPath);
        }

        void SetUpLine(List<Vector2Int> cells)
        {
            _hintLine.positionCount = cells.Count;
            _hintLine.startWidth = 0.2f;
            for (var i = 0; i < cells.Count; i++)
            {
                _hintLine.SetPosition(i, new Vector3(cells[i].x, cells[i].y, 0));
            }
        }
    }

    public void BugAutoMovement()
    {
        if (foundPath == null || foundPath.Count==0)
        {
            Hint();
        }
        
        if (foundPath != null &&foundPath.Count>0)
        {
            _bugMovement.Move(foundPath);
        }
    }

    public void BackToLevelSelection()
    {
        SceneManager.LoadScene(levelSelectionScene);
    }
}