using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public SceneManager sceneManager;
    public Transform decorationParentObject;

    public GameObject[] enemyPrefabs;
    public GameObject[] decorationPrefabs;
    public GameObject portalPrefab;

    public float spawnConstraintX = 1.25f;
    public float spawnConstraintY = 2.75f;

    public float minX = -6.5f;
    public float maxX = 6.5f;
    public float minY = -5.5f;
    public float maxY = 6.5f;


    public List<GameObject> instantiatedObjects;
    public RoomNode startNode;
    public RoomNode currentNode;

    public void Start()
    {
        InitializeRoomNodes();
        InitializeRoom();
        InitializePortals();
    }
    public void InitializeRoomNodes()
    {
        startNode = new RoomNode(RoomNode.RoomType.StartRoom, true);
        currentNode = startNode;
        RoomNode newNode = startNode;

        newNode = AddLinearRooms(newNode, RoomNode.RoomType.EnemyRoom, 5, false);
        newNode = AddLinearRooms(newNode, RoomNode.RoomType.MiniBossRoom, 1, false);
    }




    public void InitializeRoom()
    {
        DestroyRoom();
        int[,] grid = currentNode.tileGrid;

        int width = grid.GetLength(0);
        int height = grid.GetLength(1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {

                if (grid[x, y] == 1)
                {
                    GameObject grass = Instantiate(decorationPrefabs[0], RoomNodeGridPositionToWorldPosition(x, y, width, height), Quaternion.identity);
                    grass.transform.SetParent(decorationParentObject, false);
                    grass.GetComponent<TilemapObjectManager>().player = sceneManager.heroGameObject.transform;
                    instantiatedObjects.Add(grass);
                }
                else if (grid[x, y] == 2)
                {
                    int enemyNum = Random.Range(0, enemyPrefabs.Length - 1);
                    GameObject enemy = Instantiate(enemyPrefabs[enemyNum], RoomNodeGridPositionToWorldPosition(x, y, width, height), Quaternion.identity);
                    EnemyController enemyController = enemy.GetComponent<EnemyController>();
                    enemyController.sceneManager = this.sceneManager;
                    enemyController.roomManager = this;

                }
            }
        }
    }
    public void InitializePortals()
    {


        if (currentNode.top != null)
        {
            InstantiatePortal(new Vector3(0, 7.5f, 0)).direction = "top";
        }
        if (currentNode.bottom != null)
        {
            InstantiatePortal(new Vector3(0, -7.5f, 0)).direction = "bottom";
        }
        if (currentNode.left != null)
        {
            InstantiatePortal(new Vector3(-7.5f, 0, 0)).direction = "left";
        }
        if (currentNode.right != null)
        {
            InstantiatePortal(new Vector3(-7.5f, 0, 0)).direction = "right";

        }
    }
    public PortalManager InstantiatePortal(Vector3 portalPosition)
    {
        GameObject portal = Instantiate(portalPrefab, portalPosition, Quaternion.identity);
        instantiatedObjects.Add(portal);
        PortalManager portalManager = portal.GetComponent<PortalManager>();
        portalManager.sceneManager = sceneManager;
        portalManager.roomManager = this;
        portalManager.currentNode = currentNode;
        return portalManager;
    }
    public RoomNode AddLinearRooms(RoomNode start, RoomNode.RoomType type, int count, bool isCleared)
    {
        RoomNode current = start;

        for (int i = 0; i < count; i++)
        {
            RoomNode next = new RoomNode(type, isCleared);
            current.top = next;
            current = next;
        }

        return current;
    }
    public Vector3 RoomNodeGridPositionToWorldPosition(int x, int y, int width, int height)
    {
        return new Vector3(x - width / 2 + 0.5f, y - height / 2 + 0.5f, 0f);
    }
    public Vector3 GetSpawnVector3()
    {
        float x = Random.Range(-spawnConstraintX + 1, spawnConstraintX - 1);
        float y = Random.Range(-spawnConstraintY + 1, spawnConstraintY - 1);

        return new Vector3(x, y, -6);
    }
    public void DestroyRoom()
    {
        foreach(GameObject instantiatedObject in instantiatedObjects)
        {
            Destroy(instantiatedObject);
        }
    }
    public void ReduceEnemyCountOnKill()
    {
        if(currentNode.enemyCount > 0)
        {
            currentNode.enemyCount--;
        }
        if(currentNode.enemyCount == 0)
        {
            InitializePortals();
        }
    }


}

/*GameObject enemy = Instantiate(enemyPrefabs[4], new Vector3(0, 0, -6), Quaternion.identity);
EnemyController enemyController = enemy.GetComponent<EnemyController>();
enemyController.sceneManager = this.sceneManager;
enemyController.roomManager = this;
enemy.GetComponent<DarkMountController>().returnHomePortal = this.returnHomePortal;*/

/*int enemyNum = Random.Range(0, enemyPrefabs.Length - 1);
GameObject enemy = Instantiate(enemyPrefabs[enemyNum], GetSpawnLocation(true), Quaternion.identity);
EnemyController enemyController = enemy.GetComponent<EnemyController>();
enemyController.sceneManager = this.sceneManager;
enemyController.roomManager = this;*/

/*Vector2 gridPos = GetRandomEmptyTile();

Vector3 worldPos = new Vector3(gridPos.x, gridPos.y, -4);


GameObject grass = Instantiate(grassPrefab, worldPos, Quaternion.identity);
grass.transform.SetParent(tilemapParent.transform, false);
grass.GetComponent<TilemapObjectManager>().player = sceneManager.heroGameObject.transform;
occupiedTiles.Add(gridPos);*/