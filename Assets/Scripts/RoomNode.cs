using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNode
{
    public int roomType; // 0 = Empty, 1 = Start Room, 2 = Enemy Room, 3 = Boss Room
    public RoomNode top, bottom, left, right; // room connections

    public Vector2Int gridPosition; // Position in the 2D array

    public RoomNode(int type, Vector2Int position)
    {
        this.roomType = type;
        gridPosition = position;
    }
}
