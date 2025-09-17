using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    List<RoomNode> map = new List<RoomNode>();
    public RoomNode currentRoom;

    public SceneManager sceneManager;
    public EntranceManager entranceManager;
    public List<GameObject> instantiatedObjects;
    public List<GameObject> portals;
    public GameObject weaponDropPrefab;
    public List<GameObject> createdObjectsInScene;
    public List<GameObject> biomeOneEnemies;
    public List<GameObject> biomeTwoEnemies;
    public List<GameObject> biomeThreeEnemies;

    void Start()
    {
        GenerateMap();
        currentRoom = map[0];
        
        CreateRoomInScene();
    }

    void Update()
    {
        
    }
    public void CreateRoomInScene()
    {
        DestroyCreatedObjectsInScene();
        foreach (GameObject portal in portals)
        {
            portal.SetActive(false);
        }
        InstantiateRoom();
        DropItems();
    }
    public void DestroyCreatedObjectsInScene()
    {
        foreach( GameObject createdObjects in createdObjectsInScene)
        {
            Destroy(createdObjects);
        }
    }
    public void InstantiateRoom()
    {
        int width = currentRoom.tileGrid.GetLength(0);
        int height = currentRoom.tileGrid.GetLength(1);

        Vector2 worldPos = new Vector2(-9, -9);
        
        for (int x = 0; x < width; x++)
        {
            worldPos.y = -9;

            for (int y = 0; y < height; y++)
            {
                if (currentRoom.tileGrid[x, y] == 1)
                {
                    GameObject grassObject = Instantiate(instantiatedObjects[0], worldPos, Quaternion.identity);
                    createdObjectsInScene.Add(grassObject);
                }
                if (currentRoom.tileGrid[x, y] == 2)
                {
                    GameObject flowerObject = Instantiate(instantiatedObjects[UnityEngine.Random.Range(1, 4)], worldPos, Quaternion.identity);
                    createdObjectsInScene.Add(flowerObject);
                }
                if (currentRoom.tileGrid[x, y] == 3)
                {
                    GameObject tallGrassObject = Instantiate(instantiatedObjects[4], worldPos, Quaternion.identity);
                    createdObjectsInScene.Add(tallGrassObject);
                }
                if (currentRoom.tileGrid[x, y] == 4)
                {
                    SpawnEnemies(currentRoom, x, y);


                }
                worldPos.y += 1;
            }
            worldPos.x += 1;
        }
        SpawnBoss1();
        SpawnBoss2();
        SpawnBoss3();
    }

    public void SpawnBoss1() { 
        
        if(currentRoom.roomType == RoomNode.RoomType.BossRoom1)
        {
            SpawnSingleEnemy(biomeOneEnemies, 3);
        }
    }
    public void SpawnBoss2()
    {

        if (currentRoom.roomType == RoomNode.RoomType.BossRoom2)
        {
            SpawnSingleEnemy(biomeTwoEnemies, 3);
        }
    }
    public void SpawnBoss3()
    {

        if (currentRoom.roomType == RoomNode.RoomType.BossRoom3)
        {
            SpawnSingleEnemy(biomeThreeEnemies, 4);
        }
    }
    public void SpawnSingleEnemy(List<GameObject> biomeEnemiesList, int x, int y)
    {
        GameObject enemy = Instantiate(biomeEnemiesList[UnityEngine.Random.Range(0, biomeEnemiesList.Count - 1)], RoomNodeGridPositionToWorldPosition(x, y, 20, 20), Quaternion.identity);
        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        enemyController.sceneManager = this.sceneManager;
    }
    public void SpawnSingleEnemy(List<GameObject> biomeEnemiesList, int enemyNumber)
    {
        GameObject enemy = Instantiate(biomeEnemiesList[enemyNumber], new Vector3(0, 0, 0), Quaternion.identity);
        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        enemyController.sceneManager = this.sceneManager;
    }
    public void SpawnEnemies(RoomNode node, int x, int y)
    {
        if(node.roomType != RoomNode.RoomType.EnemyRoom)
        {
            return;
        }

        if(node.biome == "Slime Village")
        {
            SpawnSingleEnemy(biomeOneEnemies, x, y);
        }
        if (node.biome == "Mushroom Forest")
        {
            SpawnSingleEnemy(biomeTwoEnemies, x, y);

        }
        if (node.biome == "Cybernetic Enclave")
        {
            SpawnSingleEnemy(biomeThreeEnemies, x, y);
        }
    }
    public void DropItems()
    {
        foreach (String item in currentRoom.itemDrops.Keys)
        {
            GameObject weaponDrop = Instantiate(weaponDropPrefab, currentRoom.itemDrops[item], Quaternion.identity);
            weaponDrop.GetComponent<WeaponDropManager>().weaponName = item;
            createdObjectsInScene.Add(weaponDrop);
        }
    }


    void GenerateMap()
    {
        GenerateMainPath();
    }
    void GenerateMainPath()
    {
        RoomNode startRoom = new RoomNode(RoomNode.RoomType.StartRoom);
        startRoom.DefineRoom();
        startRoom.biome = "Slime Village";
        map.Add(startRoom);


        RoomNode current = startRoom;


        int roomsCreated = 1;

        int totalRooms = 12;



        while (roomsCreated <= totalRooms)
        {

            RoomNode.RoomType type = RoomNode.RoomType.EnemyRoom;

            if (roomsCreated == 13 || roomsCreated == 18 || roomsCreated == 28)//8 18 // 28
            {
                type = RoomNode.RoomType.ShopRoom;
            }

            if (roomsCreated == 4)
            {
                type = RoomNode.RoomType.BossRoom1;
            }
            if (roomsCreated == 8)
            {
                type = RoomNode.RoomType.BossRoom2;
            }
            if (roomsCreated == 12)
            {
                type = RoomNode.RoomType.BossRoom3;
            }


            RoomNode next = new RoomNode(type);
            if (roomsCreated < 4)
            {
                next.biome = "Slime Village";
            }
            if (roomsCreated < 8 && roomsCreated >= 4)
            {
                next.biome = "Mushroom Forest";
            }
            if (roomsCreated < 12 && roomsCreated >= 8)
            {
                next.biome = "Cybernetic Enclave";
            }

            current.top = next;
            next.bottom = current;
            current = next;
            current.DefineRoom();
            map.Add(current);

            roomsCreated++;
        }
    }






    public void ChangeRoom(RoomNode nextRoom)
    {
        if (currentRoom == nextRoom) return;
        currentRoom = nextRoom;
        CreateRoomInScene();
    }




    public Vector3 RoomNodeGridPositionToWorldPosition(int x, int y, int width, int height)
    {
        return new Vector3(x - width / 2 + 0.5f, y - height / 2 + 0.5f, 0f);
    }
}
