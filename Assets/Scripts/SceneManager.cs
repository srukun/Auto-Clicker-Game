using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    //Gold Management
    public int goldEarned;
    public GameObject Text_GoldEarned;


    //Spawn Management
    public GameObject[] enemyPrefabs;
    public GameObject[] spawnPoints;
    public int enemiesRemaining;
    public int waveNumber;

    public bool signalActive;
    public float spawnDelayTimer;
    public GameObject notificationText;
    public GameObject SceneObj_Canvas;
    public bool warningNotification;

    public GameObject enemyInformationText;
    public GameObject SceneObject_Hero;
    //Skins
    public GameObject[] skins;

    //Scene
    public GameObject SceneObject_Camera;
    public GameObject SceneObject_MapManager;
    void Start()
    {
        signalActive = true;
        spawnDelayTimer = 2.5f;
        waveNumber++;
        SceneObject_MapManager.GetComponent<MapManager>().GenerateMap();
        InitializePlayer();

        HeroController heroController = SceneObject_Hero.GetComponent<HeroController>();
        MapManager mapManager = SceneObject_MapManager.GetComponent<MapManager>();
        heroController.SetStartRoom(mapManager.GetStartRoom());

    }


    void Update()
    {
        
    }
    public void InitializePlayer()
    {
        GameObject heroObject = Instantiate(skins[0], new Vector3(0, -2.65f, -2), Quaternion.identity);
        heroObject.GetComponentInChildren<KnifeController>().SceneObject_Camera = SceneObject_Camera;
        this.SceneObject_Hero = heroObject;
    }
    public void IncreaseEarnedGold(int gold)
    {
        goldEarned += gold;
        DataManager.totalGold += gold;
        Text_GoldEarned.GetComponent<TextMeshProUGUI>().SetText(goldEarned + "");
    }


    public void SpawnEnemy(int enemyCode, int spawnCode)
    {

        GameObject SceneObject_Enemy = Instantiate(enemyPrefabs[enemyCode], spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)].transform.position, Quaternion.identity);
        SceneObject_Enemy.GetComponent<EnemyController>().thisEnemy.AssignLevel(Random.Range(waveNumber, waveNumber + 2));
        SceneObject_Enemy.GetComponent<EnemyController>().SceneObject_ArenaManager = gameObject;
        SceneObject_Enemy.GetComponent<EnemyController>().SceneObj_Canvas = SceneObj_Canvas;
        SceneObject_Enemy.GetComponent<EnemyController>().enemyInformationTextPrefab = enemyInformationText;
        SceneObject_Enemy.GetComponent<EnemyController>().spawnCode= spawnCode;
        SceneObject_Enemy.GetComponent<EnemyController>().SceneObject_Hero = this.SceneObject_Hero;
        enemiesRemaining++;

    }
}
