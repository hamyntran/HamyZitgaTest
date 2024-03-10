
using UnityEngine;

public class MazeRoom : MonoBehaviour
{
    [SerializeField] private GameObject topWall, bottomWall, leftWall, rightWall;
    
    public bool IsVisited { get; private set; }

    public void Visit()
    {
        IsVisited = true;
    }

    public void ClearWall(RoomDirection direction)
    {
        switch (direction)
        {
            case RoomDirection.TOP:
                topWall.SetActive(false);
                break;
            case RoomDirection.BOTTOM:
                bottomWall.SetActive(false);
                break;
            case RoomDirection.LEFT:
                leftWall.SetActive(false);
                break;
            case RoomDirection.RIGHT:
                rightWall.SetActive(false);
                break;
        }
    }

    public bool CheckPassable(RoomDirection direction)
    {
        switch(direction)
        {
            case RoomDirection.TOP:
                return !topWall.activeSelf;
            case RoomDirection.BOTTOM:
                return !bottomWall.activeSelf;
            case RoomDirection.LEFT:
                return !leftWall.activeSelf;
            case RoomDirection.RIGHT:
                return !rightWall.activeSelf;
            default:
                return false;
        }
    }
}


public enum RoomDirection
{
    TOP,
    BOTTOM,
    LEFT,
    RIGHT
}
