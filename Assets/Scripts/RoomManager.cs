using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public SceneManager sceneManager;

    //Start Room

    //Enemy Room
    public GameObject[] enemyPrefabs;
    public float spawnConstraintX = 1.25f;
    public float spawnConstraintY = 2.75f;
    public int numEnemies;
    public GameObject Prefab_Portal;
    public List<GameObject> portals;
    public GameObject[] tilemap;

    //Room Generation
    public GameObject treePrefab;
    public GameObject grassPrefab;
    public GameObject tilemapParent;
    public int gridWidth = 6;
    public int gridHeight = 10;

    public float minX = -6.5f;
    public float maxX = 6.5f;
    public float minY = -5.5f;
    public float maxY = 6.5f;
    public float tileSize = 1f;

    public GameObject returnHomePortal;
    private HashSet<Vector2> occupiedTiles = new HashSet<Vector2>();
    public void SpawnEnemies()
    {
        if (sceneManager.IsRoomCleared(sceneManager.currentPosition))
        {
            return;
        }
        numEnemies = Random.Range(1, 4);
        for(int i = 0; i < numEnemies; i++)
        {
            //Random.Range(0, enemyPrefabs.Length)
            GameObject enemy = Instantiate(enemyPrefabs[2], GetSpawnLocation(true), Quaternion.identity);
            BaseEnemyController enemyController = enemy.GetComponent<BaseEnemyController>();
            enemyController.sceneManager = this.sceneManager;
            enemyController.roomManager = this;

        }
    }
    public Vector3 GetSpawnLocation()
    {
        float x = Random.Range(-spawnConstraintX, spawnConstraintX);
        float y = Random.Range(-spawnConstraintY, spawnConstraintY);

        return new Vector3(x, y, -1);
    }
    public Vector3 GetSpawnLocation(bool enemy)
    {
        float x = Random.Range(-spawnConstraintX, spawnConstraintX);
        float y = Random.Range(-spawnConstraintY, spawnConstraintY);

        return new Vector3(x, y, -6);
    }

    public void RoomSetUp()
    {
        SetPortalsInactive();
        GenerateRoom();
        int x = sceneManager.currentPosition.x;
        int y = sceneManager.currentPosition.y;

        if (sceneManager.levelMap[x,y] == 1)
        {
            StartRoomSetUp();
        }
        else if (sceneManager.levelMap[x, y] == 2)
        {
            EnemyRoomSetUp();
        }
        if (sceneManager.levelMap[x, y] == 3)
        {
            GameObject enemy = Instantiate(enemyPrefabs[4], new Vector3(0, 0, -6), Quaternion.identity);
            BaseEnemyController enemyController = enemy.GetComponent<BaseEnemyController>();
            enemyController.sceneManager = this.sceneManager;
            enemyController.roomManager = this;
            enemy.GetComponent<DarkMountController>().returnHomePortal = this.returnHomePortal;
        }
    }
    public void GenerateRoom()
    {
        SelectTilemap();
        ResetRoom();
    }
    public void ResetRoom()
    {
        foreach (Transform child in tilemapParent.transform)
        {
            Destroy(child.gameObject);
        }

        occupiedTiles.Clear();

        GenerateGrass();
        GenerateTrees();
        
    }
    public void SelectTilemap()
    {
        foreach(GameObject tilemap in tilemap)
        {
            tilemap.SetActive(false);
        }
        //tilemap[Random.Range(0, tilemap.Length)].SetActive(true);
        tilemap[0].SetActive(true);
    }
    void GenerateGrass()
    {
        int grassCount = Random.Range(15, 20); // 4-6 grass
        for (int i = 0; i < grassCount; i++)
        {
            Vector2 gridPos = GetRandomEmptyTile();

            Vector3 worldPos = new Vector3(gridPos.x, gridPos.y, -4);
            

            GameObject grass = Instantiate(grassPrefab, worldPos, Quaternion.identity);
            grass.transform.SetParent(tilemapParent.transform, false);
            grass.GetComponent<TilemapObjectManager>().player = sceneManager.heroGameObject.transform;
            occupiedTiles.Add(gridPos);
        }
    }

    void GenerateTrees()
    {
        int treeCount = Random.Range(10, 15); // 3-4 trees
        for (int i = 0; i < treeCount; i++)
        {
            Vector2 gridPos = GetRandomEmptyTile();

            // Ensure tree fits within bounds (2 tiles tall)
            if (gridPos.y + 1 < gridHeight && !occupiedTiles.Contains(gridPos + Vector2Int.up))
            {
                Vector3 worldPos = new Vector3(gridPos.x, gridPos.y, -5);

                GameObject tree = Instantiate(treePrefab, worldPos, Quaternion.identity);
                tree.transform.SetParent(tilemapParent.transform, false);
                tree.GetComponent<TilemapObjectManager>().player = sceneManager.heroGameObject.transform;
                occupiedTiles.Add(gridPos);
                occupiedTiles.Add(gridPos + Vector2Int.up);
            }
            else
            {
                i--; // Retry if placement is invalid
            }
        }
    }



    public Vector2 GetRandomEmptyTile()
    {
        Vector2 randomTile;
        int maxAttempts = 1000;
        int attempts = 0;

        do
        {
            float randomX = Mathf.Round(Random.Range(minX, maxX));
            float randomY = Mathf.Round(Random.Range(minY, maxY));

            randomTile = new Vector2(randomX + 0.5f, randomY + 0.5f);
            attempts++;

        } while (occupiedTiles.Contains(randomTile) && attempts < maxAttempts);

        if (attempts >= maxAttempts)
        {
            return Vector2.zero;
        }

        occupiedTiles.Add(randomTile);

        return randomTile;
    }
    public void StartRoomSetUp()
    {
        SetPortalsActive();
    }
    public void EnemyRoomSetUp()
    {
        SetPortalsInactive();
        SpawnEnemies();
    }
    public void TreasureRoomSetUp()
    {

    }
    public void BossRoomSetUp()
    {

    }
    public void SetPortalsInactive()
    {
        foreach (GameObject portal in portals)
        {
            portal.SetActive(false);
        }
    }
    public void SetPortalsActive()
    {

        int x = sceneManager.currentPosition.x;
        int y = sceneManager.currentPosition.y;
        int[,] levelMap = sceneManager.levelMap;

        int rows = levelMap.GetLength(0);
        int cols = levelMap.GetLength(1);


        if (x - 1 >= 0 && levelMap[x - 1, y] != 0) // Top
        {
            portals[0].SetActive(true);
        }
        if (x + 1 < cols && levelMap[x + 1, y] != 0) // Bottom
        {
            portals[1].SetActive(true);
        }
        if (y - 1 >= 0 && levelMap[x, y - 1] != 0) // Left
        {
            portals[2].SetActive(true);
        }
        if (y + 1 < rows && levelMap[x, y + 1] != 0) // Right
        {
            portals[3].SetActive(true);
        }
    }



    public void DecreaseEnemiesRemaining()
    {
        numEnemies--;
        if(numEnemies == 0)
        {
            SetPortalsActive();
            sceneManager.MarkRoomAsCleared(sceneManager.currentPosition);
        }

    }

}
