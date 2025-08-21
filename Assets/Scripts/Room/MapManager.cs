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
        entranceManager.UpdateEntrances();
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
                    GameObject enemy = Instantiate(instantiatedObjects[4], worldPos, Quaternion.identity);
                }
                worldPos.y += 1;
            }
            worldPos.x += 1;
        }
    }
    public void SpawnEnemies()
    {
        GameObject enemy = Instantiate(biomeOneEnemies[UnityEngine.Random.Range(0, biomeOneEnemies.Count)], new Vector3(0, 0, 0), Quaternion.identity);
        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        enemyController.sceneManager = this.sceneManager;
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

        map.Add(startRoom);


        RoomNode current = startRoom;


        int roomsCreated = 1;
        int totalRooms = 30;

        while (roomsCreated <= totalRooms)
        {

            RoomNode.RoomType type = RoomNode.RoomType.EnemyRoom;

            if (roomsCreated == 8 || roomsCreated == 18 || roomsCreated == 28)
            {
                type = RoomNode.RoomType.ShopRoom;
            }
            if (roomsCreated == 10)
            {
                type = RoomNode.RoomType.BossRoom1;
            }
            if (roomsCreated == 20)
            {
                type = RoomNode.RoomType.BossRoom2;
            }
            if (roomsCreated == 30)
            {
                type = RoomNode.RoomType.BossRoom2;
            }
            

            RoomNode next = new RoomNode(type);

            current.top = next;
            next.bottom = current;
            current = next;
            map.Add(current);

            roomsCreated++;
            current.DefineRoom();
        }
    }





    private  (int, int) BiasedDirectionChooser()
    {
        float roll = UnityEngine.Random.value;
        if(roll < 0.7f)
        {
            return (0, 1);
        }
        else if(roll < 0.85f)
        {
            return (0, -1);
        }
        return (-1, 0);
    }
    public void ChangeRoom(RoomNode nextRoom)
    {
        if (currentRoom == nextRoom) return;

        DestroyCurrentRoomObjects();

        SpawnRoom(nextRoom);

        currentRoom = nextRoom;
    }

    private void SpawnRoom(RoomNode nextRoom)
    {
        throw new NotImplementedException();
    }

    private void DestroyCurrentRoomObjects()
    {
        throw new NotImplementedException();
    }
    public Vector3 RoomNodeGridPositionToWorldPosition(int x, int y, int width, int height)
    {
        return new Vector3(x - width / 2 + 0.5f, y - height / 2 + 0.5f, 0f);
    }
}
