using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class KnifeClass 
{
    public string knifeName;
    public int damage;
    public int cost;
    public float speed;
    public bool isBought;
    public int fireRate;
    public int level;
    public int levelUpCost;
    public KnifeClass(string knifeName, int damage, int cost, float speed, bool isBought, int fireRate)
    {
        this.level = 1;
        this.levelUpCost = 5;
        this.knifeName = knifeName;
        this.damage = damage;
        this.cost = cost;
        this.speed = speed;
        this.isBought = isBought;
        this.fireRate = fireRate;
    }
    public int getDamage() { return damage; }
    public void LevelUp()
    {
        this.level++;
        this.damage++;
        levelUpCost *= 2;
    }
}
