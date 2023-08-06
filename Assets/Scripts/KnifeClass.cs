using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class KnifeClass 
{
    public string kinfeName;
    public int damage;
    public int cost;
    public float speed;
    public bool isBought;
    public int fireRate;
    public KnifeClass(string knifeName, int damage, int cost, float speed, bool isBought, int fireRate)
    {
        this.kinfeName = knifeName;
        this.damage = damage;
        this.cost = cost;
        this.speed = speed;
        this.isBought = isBought;
        this.fireRate = fireRate;
    }
    public int getDamage() { return damage; }
}
