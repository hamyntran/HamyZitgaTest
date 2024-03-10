using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Pathfinding 
{
    public Cell[,] grid;
    public MazeRoom[,] mazeGrid;

    public Pathfinding(MazeRoom[,] grid)
    {
        mazeGrid = grid;
        int width = grid.GetLength(0);
        int height = grid.GetLength(1);
        this.grid = new Cell[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                this.grid[x, y] = new Cell(x, y);
            }
        }
    }

    public List<Vector2Int> FindPath(Vector2Int startPos, Vector2Int targetPos)
    {
        Cell startCell = grid[startPos.x, startPos.y];
        Cell targetCell = grid[targetPos.x, targetPos.y];

        List<Cell> openList = new List<Cell>();
        HashSet<Cell> closedList = new HashSet<Cell>();

        openList.Add(startCell);

        while (openList.Count > 0)
        {
            //Get cell with lowest cost value
            Cell cell = openList[0];
            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].ValueF <= cell.ValueF && openList[i].ValueH < cell.ValueH)
                {
                    cell = openList[i];
                }
            }

            //Move selected cell from open to close list
            openList.Remove(cell);
            closedList.Add(cell);

            //If selected cell is End cell
            if (cell == targetCell)
            {
                //Return whole path
                return RetracePath(startCell, targetCell);
            }

            //Loop through all neighbours of selected cell
            foreach (Cell neighbour in GetNeighbours(grid, cell))
            {
                //Skip if neighbour is not walkable or in close list
                if (closedList.Contains(neighbour))
                {
                    continue;
                }

                //Get cost
                float costToNeighbour = cell.ValueG + GetDistance(cell, neighbour);

                if (costToNeighbour < neighbour.ValueG || !openList.Contains(neighbour))
                {
                    neighbour.SetG(costToNeighbour);
                    neighbour.SetH(neighbour);
                    neighbour.SetParent(cell);

                    if (!openList.Contains(neighbour))
                    {
                        openList.Add(neighbour);
                    }
                }
            }
        }

        return null;
    }

    private List<Vector2Int> RetracePath(Cell startCell, Cell endcCell)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Cell currentCell = endcCell;
        Vector2 recentDir = Vector2.zero;

        while (currentCell != startCell)
        {
            Vector2 currentDir = new Vector2(currentCell.X - currentCell.Parent.X,
                currentCell.Y - currentCell.Parent.Y);

            if (recentDir != currentDir)
            {
                path.Add(new Vector2Int(currentCell.X,currentCell.Y ));
            }

            recentDir = currentDir;
            currentCell = currentCell.Parent;
        }
        
        path.Add(new Vector2Int(startCell.X,startCell.Y));
            path.Reverse();

        return path;
    }


    private int GetDistance(Cell cellA, Cell cellB)
    {
        int dstX = Mathf.Abs(cellA.X - cellB.X);
        int dstY = Mathf.Abs(cellA.Y - cellB.Y);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }

    public List<Cell> GetNeighbours(Cell[,] grid, Cell cell)
    {
        List<Cell> neighbours = new List<Cell>();

        //Above
        if (cell.Y + 1 < grid.GetLength(1) && mazeGrid[cell.X, cell.Y].CheckPassable(RoomDirection.TOP) &&
            mazeGrid[cell.X, cell.Y + 1].CheckPassable(RoomDirection.BOTTOM))
        {
            neighbours.Add(grid[cell.X, cell.Y + 1]);
        }

        //Under
        if (cell.Y - 1 >= 0 && mazeGrid[cell.X, cell.Y].CheckPassable(RoomDirection.BOTTOM) &&
            mazeGrid[cell.X, cell.Y - 1].CheckPassable(RoomDirection.TOP))
        {
            neighbours.Add(grid[cell.X, cell.Y - 1]);
        }

        //Left
        if (cell.X - 1 >= 0 && mazeGrid[cell.X, cell.Y].CheckPassable(RoomDirection.LEFT) &&
            mazeGrid[cell.X - 1, cell.Y].CheckPassable(RoomDirection.RIGHT))
        {
            neighbours.Add(grid[cell.X - 1, cell.Y]);
        } 
        
        //Right
        if (cell.X + 1 < grid.GetLength(0) && mazeGrid[cell.X, cell.Y].CheckPassable(RoomDirection.RIGHT) &&
            mazeGrid[cell.X + 1, cell.Y].CheckPassable(RoomDirection.LEFT))
        {
            neighbours.Add(grid[cell.X + 1, cell.Y]);
        }

        return neighbours;
    }
    
    public class  Cell
    {
        private int _x;
        private int _y;

        private float _g;
        private float _h;

        private Cell _parent;
        public float ValueF => _g + _h;
        public int X => _x;
        public int Y => _y;

        public float ValueH => _h;
        public float ValueG => _g;
        public Cell Parent => _parent;


        public Cell(int x, int y)
        {
            this._x = x;
            this._y = y;
        }

        public void SetG(float ig)
        {
            _g = ig;
        }

        public void SetH(Cell goal)
        {
            _h = Vector2Int.Distance(new Vector2Int(_x, _y), new Vector2Int(goal.X, goal.Y));
        }

        public void SetParent(Cell parent)
        {
            this._parent = parent;
        }
    }
}


