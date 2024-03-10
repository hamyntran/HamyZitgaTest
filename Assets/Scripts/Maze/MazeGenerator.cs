
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] private MazeRoom _roomPrefab;
    [SerializeField] private int _width, _height;

    private MazeRoom[,] _roomGrid;

    public MazeRoom[,] RoomGrid => _roomGrid;
        public int Width => _width;
    public int Height => _height;

    public void Generate()
    {
        _roomGrid = new MazeRoom[_width, _height];

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                _roomGrid[x, y] = Instantiate(_roomPrefab, new Vector3(x, y, 0), quaternion.identity);
                _roomGrid[x, y].name = "Room(" + x + "," + y + ")";
                _roomGrid[x, y].transform.SetParent(transform);
            }
        }

        GenerateMaze(null, _roomGrid[0, Height - 1]);
    }

    void GenerateMaze(MazeRoom previousRoom, MazeRoom currentRoom)
    {
        currentRoom.Visit();
        UpdateWalls(previousRoom, currentRoom);

        MazeRoom nextRoom = null;

        do
        {
            nextRoom = GetUnvisitedRoom(currentRoom);

            if (nextRoom != null)
            {
                GenerateMaze(currentRoom, nextRoom);
            }
        } while (nextRoom != null);
    }
    

    private List<MazeRoom> GetUnvisitedRooms(MazeRoom currentRoom)
    {
        int x = (int)currentRoom.transform.position.x;
        int y = (int)currentRoom.transform.position.y;

        List<MazeRoom> unvistedRooms = new List<MazeRoom>();

        if (x + 1 < _width)
        {
            var rightCell = _roomGrid[x + 1, y];
            if (rightCell.IsVisited == false)
            {
                unvistedRooms.Add(rightCell);
            }
        }

        if (x - 1 >= 0)
        {
            var leftCell = _roomGrid[x - 1, y];
            if (leftCell.IsVisited == false)
            {
                unvistedRooms.Add(leftCell);
            }
        }

        if (y + 1 < _height)
        {
            var aboveCell = _roomGrid[x, y + 1];
            if (aboveCell.IsVisited == false)
            {
                unvistedRooms.Add(aboveCell);
            }
        }

        if (y - 1 >= 0)
        {
            var underCell = _roomGrid[x, y - 1];
            if (underCell.IsVisited == false)
            {
                unvistedRooms.Add(underCell);
            }
        }

        return unvistedRooms;
    }

    private MazeRoom GetUnvisitedRoom(MazeRoom currentRoom)
    {
        List<MazeRoom> unvisitedRooms = GetUnvisitedRooms(currentRoom);
        if (unvisitedRooms.Count == 0) return null;
        return unvisitedRooms[Random.Range(0, unvisitedRooms.Count)];
    }

    private void UpdateWalls(MazeRoom previousRoom, MazeRoom currentRoom)
    {
        if (previousRoom == null) return;

        //Previous room is on the left of current room
        if (previousRoom.transform.position.x < currentRoom.transform.position.x)
        {
            previousRoom.ClearWall(RoomDirection.RIGHT);
            currentRoom.ClearWall(RoomDirection.LEFT);
            return;
        }

        //Previous room is on the right of current room
        if (previousRoom.transform.position.x > currentRoom.transform.position.x)
        {
            previousRoom.ClearWall(RoomDirection.LEFT);
            currentRoom.ClearWall(RoomDirection.RIGHT);
            return;
        }

        //Previous room is above current room
        if (previousRoom.transform.position.y > currentRoom.transform.position.y)
        {
            previousRoom.ClearWall(RoomDirection.BOTTOM);
            currentRoom.ClearWall(RoomDirection.TOP);
            return;
        }

        //Previous room is underneath current room
        if (previousRoom.transform.position.y < currentRoom.transform.position.y)
        {
            previousRoom.ClearWall(RoomDirection.TOP);
            currentRoom.ClearWall(RoomDirection.BOTTOM);
            return;
        }
    }
}