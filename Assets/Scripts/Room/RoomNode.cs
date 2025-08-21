using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomNode
{
    public RoomType roomType;

    public RoomNode top;
    public RoomNode bottom;




    public int[,] tileGrid = new int[20, 20]; // 0 = empty 1 = walls 2 = grass 3 = enemy
    public int numberOfEnemies;
    public Dictionary<String, Vector3> itemDrops;
    public RoomNode(RoomType roomType)
    {
        this.roomType = roomType;
        itemDrops = new Dictionary<String, Vector3>();
        numberOfEnemies = UnityEngine.Random.Range(2, 5);
    }
    public enum RoomType
    {
        StartRoom,
        EnemyRoom,
        ShopRoom,
        BossRoom1,
        BossRoom2,
        BossRoom3,
    }
    public void DefineRoom()
    {
        DefineEnemy();
        DefineGrass();
        DefineStartRoomWeapons();
    }
    public void DefineStartRoomWeapons()
    {
        if (roomType == RoomType.StartRoom)
        {
            itemDrops.Add("Guardians Sword", new Vector3(-2, 1.5f, -3));
            itemDrops.Add("Sheepskin Bow", new Vector3(0, 1.5f, -3));
            itemDrops.Add("Steels Edge", new Vector3(2, 1.5f, -3));
        }
    }
    public void DefineGrass()
    {
        int width = tileGrid.GetLength(0);
        int height = tileGrid.GetLength(1);
        for (int x = 1; x < width - 1; x++)
        {
            for (int y = 1; y < height - 1; y++)
            {
                float chanceToSpawnGrass = UnityEngine.Random.Range(0f, 1f);

                if (chanceToSpawnGrass < 0.15f && chanceToSpawnGrass >= 0.05f && tileGrid[x, y] == 0)
                {
                    tileGrid[x, y] = 1;
                }
                if (chanceToSpawnGrass < 0.05f && chanceToSpawnGrass >= 0.01f && tileGrid[x, y] == 0)
                {
                    tileGrid[x, y] = 2;
                }
                if (chanceToSpawnGrass < 0.01 && tileGrid[x, y] == 0)
                {
                    tileGrid[x, y] = 3;
                }

            }
        }
    }
    public void DefineEnemy()
    {
        numberOfEnemies = UnityEngine.Random.Range(2, 5);
        int width = tileGrid.GetLength(0);
        int height = tileGrid.GetLength(1);

        int topSafeZone = 0;
        int bottomSafeZone = 0;
        int leftSafeZone = 0;
        int rightSafeZone = 0;

        if (bottom != null) bottomSafeZone = 5;

        int enemiesPlaced = 0;
        int maxAttempts = 1000;
        int attempts = 0;

        while (enemiesPlaced < numberOfEnemies && attempts < maxAttempts)
        {
            int x = UnityEngine.Random.Range(1 + leftSafeZone, width - 1 - rightSafeZone);
            int y = UnityEngine.Random.Range(1 + bottomSafeZone, height - 1 - topSafeZone);

            if (tileGrid[x, y] == 0)
            {
                tileGrid[x, y] = 4;
                enemiesPlaced++;
            }

            attempts++;
        }
    }

}