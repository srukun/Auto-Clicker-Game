using System.Collections.Generic;
using UnityEngine;

public class MapNode
{
    public RoomType type;
    public bool isCleared;
    public int[,] tileGrid;

    public MapNode top;
    public MapNode bottom;
    public MapNode left;
    public MapNode right;
    public int enemyCount;



    public MapNode(RoomType type, bool isCleared)
    {
        this.type = type;
        this.isCleared = isCleared;
        tileGrid = new int[14, 14];
        AddDecorationToGrid();
        if(type == RoomType.EnemyRoom)
        {
            AddEnemiesToGrid();
        }
    }

    public enum RoomType
    {
        Empty,
        StartRoom,
        EnemyRoom,
        ShopRoom,
        MiniBossRoom,
        Leaflutter,
        DarkMount,
        BossRoom
    }
    public void AddDecorationToGrid()
    {
        int width = tileGrid.GetLength(0);
        int height = tileGrid.GetLength(1);

        int decorationCount = Random.Range(20, 25);

        int attempts = 0;
        int placed = 0;

        while (placed < decorationCount && attempts < 100000)
        {
            int x = Random.Range(0, width);
            int y = Random.Range(0, height);

            if (tileGrid[x, y] == 0)
            {
                tileGrid[x, y] = 1;
                placed++;
            }
            Debug.Log("test");
            attempts++;
        }
    }
    public void AddEnemiesToGrid()
    {
        int width = tileGrid.GetLength(0);
        int height = tileGrid.GetLength(1);

        enemyCount = Random.Range(3, 7);

        int attempts = 0;
        int placed = 0;
        while (placed < enemyCount && attempts < 100000)
        {
            int x = Random.Range(0, width);
            int y = Random.Range(0, height);

            if (tileGrid[x, y] == 0)
            {
                tileGrid[x, y] = 2;
                placed++;

            }

            attempts++;
        }
    }





}
