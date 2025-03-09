using System.Collections;
using System.Collections.Generic;
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
    private HashSet<Vector2Int> occupiedTiles = new HashSet<Vector2Int>();
    public void SpawnEnemies()
    {
        numEnemies = Random.Range(1, 4);
        for(int i = 0; i < numEnemies; i++)
        {
            GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], GetSpawnLocation(), Quaternion.identity);
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            enemyController.sceneManager = this.sceneManager;
            enemyController.heroSceneObject = sceneManager.heroGameObject;
        }
    }
    public Vector3 GetSpawnLocation()
    {
        float x = Random.Range(-spawnConstraintX, spawnConstraintX);
        float y = Random.Range(-spawnConstraintY, spawnConstraintY);

        return new Vector3(x, y, -1);
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
        tilemap[Random.Range(0, tilemap.Length)].SetActive(true);
    }
    void GenerateGrass()
    {
        int grassCount = Random.Range(4, 7); // 4-6 grass
        for (int i = 0; i < grassCount; i++)
        {
            Vector2Int gridPos = GetRandomEmptyTile();

            Vector3 worldPos = new Vector3(gridPos.x + 0.5f, gridPos.y + 1.5f, -5);
            // Convert gridPos (row, column) to world position
            

            GameObject grass = Instantiate(grassPrefab, worldPos, Quaternion.identity);
            grass.transform.SetParent(tilemapParent.transform, false);
            grass.GetComponent<TilemapObjectManager>().player = sceneManager.heroGameObject.transform;
            occupiedTiles.Add(gridPos);
        }
    }

    void GenerateTrees()
    {
        int treeCount = Random.Range(3, 5); // 3-4 trees
        for (int i = 0; i < treeCount; i++)
        {
            Vector2Int gridPos = GetRandomEmptyTile();

            // Ensure tree fits within bounds (2 tiles tall)
            if (gridPos.y + 1 < gridHeight && !occupiedTiles.Contains(gridPos + Vector2Int.up))
            {
                Vector3 worldPos = new Vector3(gridPos.x + 0.5f, gridPos.y + 1.5f, -5);

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

    Vector2Int GetRandomEmptyTile()
    {
        Vector2Int position;
        do
        {
            position = new Vector2Int(Random.Range(0, gridWidth), Random.Range(0, gridHeight));
        }
        while (occupiedTiles.Contains(position)); // Ensure no overlap

        return position;
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
        }

    }

}
