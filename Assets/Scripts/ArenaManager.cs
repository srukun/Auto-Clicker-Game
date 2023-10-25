using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArenaManager : MonoBehaviour
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

    void Start()
    {
        signalActive = true;
        spawnDelayTimer = 2.5f;
        waveNumber++;
    }


    void Update()
    {
        SpawnDealyManager();
    }
    public void IncreaseEarnedGold(int gold)
    {
        goldEarned += gold;
        DataManager.totalGold += gold;
        Text_GoldEarned.GetComponent<TextMeshProUGUI>().SetText(goldEarned + "");
    }
    public void SpawnDealyManager()
    {
        if(signalActive && spawnDelayTimer > 0)
        {
            spawnDelayTimer -= Time.deltaTime;
        }
        if(spawnDelayTimer <= 0)
        {
            NewWave();
            spawnDelayTimer = 2.5f;
            signalActive = false;
            warningNotification = false;
            GameObject UIObj_WarningNotification = Instantiate(notificationText);
            UIObj_WarningNotification.GetComponent<UITextController>().SetNotification(1.5f, "KILL ALL ENEMIES");
            UIObj_WarningNotification.transform.SetParent(SceneObj_Canvas.transform, false);
            UIObj_WarningNotification.transform.localScale = Vector3.one;
            UIObj_WarningNotification.transform.localPosition = new Vector3(0, 250, 0);
        }
        if (spawnDelayTimer <= 2 && spawnDelayTimer >= 0 && !warningNotification)
        {
            warningNotification = true;
            GameObject UIObj_WarningNotification = Instantiate(notificationText);
            UIObj_WarningNotification.GetComponent<UITextController>().SetNotification(1.75f, "ENEMIES INCOMING");
            UIObj_WarningNotification.transform.SetParent(SceneObj_Canvas.transform, false);
            UIObj_WarningNotification.transform.localScale = Vector3.one;
            UIObj_WarningNotification.transform.localPosition = new Vector3(0, 150, 0);

        }
    }
    public void NewWave()
    {
        if(enemiesRemaining != 0) { return; }

        if(waveNumber >= 1 && waveNumber <= 2)
        {
            SpawnEnemy(0, 1);
        }
        else if(waveNumber >= 3 && waveNumber <= 4)
        {
            int a = 0,  b = 0;
            while(a == b)
            {
                a = Random.Range(0, 2);
                b = Random.Range(0, 2);
            }
            SpawnEnemy(Random.Range(0, enemyPrefabs.Length), a);
            SpawnEnemy(Random.Range(0, enemyPrefabs.Length), b);

        }
        else
        {
            SpawnEnemy(Random.Range(0, enemyPrefabs.Length), 0);
            SpawnEnemy(Random.Range(0, enemyPrefabs.Length), 1);
            SpawnEnemy(Random.Range(0, enemyPrefabs.Length), 2);
        }
        waveNumber++;

    }
    public void SpawnEnemy(int enemyCode, int spawnCode)
    {
        GameObject SceneObject_Enemy = Instantiate(enemyPrefabs[enemyCode], spawnPoints[spawnCode].transform.position, Quaternion.identity);
        SceneObject_Enemy.GetComponent<EnemyController>().thisEnemy.AssignLevel(Random.Range(waveNumber, waveNumber + 2));
        SceneObject_Enemy.GetComponent<EnemyController>().SceneObject_ArenaManager = gameObject;
        SceneObject_Enemy.GetComponent<EnemyController>().SceneObj_Canvas = SceneObj_Canvas;
        SceneObject_Enemy.GetComponent<EnemyController>().enemyInformationTextPrefab = enemyInformationText;
        SceneObject_Enemy.GetComponent<EnemyController>().spawnCode= spawnCode;
        enemiesRemaining++;

    }
}
