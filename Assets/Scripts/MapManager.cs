using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapManager : MonoBehaviour
{

    public List<int[,]> levelMaps = new List<int[,]>();
    public Dictionary<Vector2Int, RoomNode> roomGraph = new Dictionary<Vector2Int, RoomNode>();
    public void Start()
    {
        List<string[]> rawMaps = new List<string[]>
        {
            new string[] { "00030", "00220", "00200", "00200", "00100" },
            new string[] { "30000", "22000", "02000", "02000", "12000" },
            new string[] { "01000", "02000", "02000", "02220", "00030" },
            new string[] { "00000", "00000", "12000", "02030", "02220" },
            new string[] { "00000", "00000", "03001", "02002", "02222" }
        };
        foreach (var rawMap in rawMaps)
        {
            levelMaps.Add(ConvertTo2DArray(rawMap));
        }
    }
    public RoomNode GetStartRoom()
    {
        foreach (var roomNode in roomGraph.Values)
        {
            if(roomNode.roomType == 1)
            {
                return roomNode;
            }
        }
        return null;
    }

    private int[,] ConvertTo2DArray(string[] mapStrings)
    {
        int size = mapStrings.Length;
        int[,] map = new int[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                map[i, j] = mapStrings[i][j] - '0'; // Convert char to int
            }
        }

        return map;
    }
    public void GenerateMap()
    {
        roomGraph.Clear();

        if (levelMaps == null || levelMaps.Count == 0)
        {
            Debug.Log("No maps available in levelMaps!");
            return;
        }

        int randomIndex = UnityEngine.Random.Range(0, levelMaps.Count);
        int[,] levelData = levelMaps[randomIndex];


        int rows = levelData.GetLength(0);
        int cols = levelData.GetLength(1);

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                int roomType = levelData[y, x];
                if (roomType != 0) // Only create rooms for 1, 2, 3
                {
                    Vector2Int position = new Vector2Int(x, y);
                    RoomNode newRoom = new RoomNode(roomType, position);
                    roomGraph[position] = newRoom;
                }
            }
        }

        //Connect Each room
        foreach (var room in roomGraph.Values)
        {
            Vector2Int pos = room.gridPosition;

            if (roomGraph.ContainsKey(pos + Vector2Int.up)) room.top = roomGraph[pos + Vector2Int.up];
            if (roomGraph.ContainsKey(pos + Vector2Int.down)) room.bottom = roomGraph[pos + Vector2Int.down];
            if (roomGraph.ContainsKey(pos + Vector2Int.left)) room.left = roomGraph[pos + Vector2Int.left];
            if (roomGraph.ContainsKey(pos + Vector2Int.right)) room.right = roomGraph[pos + Vector2Int.right];
        }

        
    }



}
