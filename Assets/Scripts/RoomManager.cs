using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    //Start Room

    //Enemy Room
    public GameObject[] enemyPrefabs;
    public float spawnConstraintX;
    public float spawnConstraintY;
    public int numEnemies;

    public void SpawnEnemies()
    {
        numEnemies = Random.Range(1, 4);
        for(int i = 0; i < numEnemies; i++)
        {
            GameObject enemy = Instantiate(enemyPrefabs[0], GetSpawnLocation(), Quaternion.identity);
        }
    }
    public Vector3 GetSpawnLocation()
    {
        return new Vector3(0, 0, 0);
    }

}
