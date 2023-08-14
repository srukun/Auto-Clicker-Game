using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EnemyClass 
{
    public string enemyName;
    public int level;
    public int health;
    public int goldRewarded;
    public EnemyClass(string enemyName, int level)
    {
        this.level = level;
        this.enemyName = enemyName;
        health = 5 + level * 2;
        goldRewarded = 5 + level * 2;
    }
    public void ModifyHealth(int modifyNumber)
    {
        health -= modifyNumber;
    }
    public int GetGoldRewarded()
    {
        return goldRewarded;
    }
    public void AssignLevel(int level)
    {
        this.level = level;
        health = 5 + level * 2;
        goldRewarded = 5 + level * 2;
    }
}
