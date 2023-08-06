using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyClass thisEnemy;
    public GameObject[] hearts;
    public int originalHealth;
    public GameObject pixelHeartExplosionEffect;
    public GameObject orangeExplosionEffect;
    public GameObject flashEffect;
    public float flashEffectTimer;
    void Start()
    {
        originalHealth = thisEnemy.health; 
    }

    void Update()
    {
        FlashEffect();
    }
    public void KillThisEnemy()
    {
        if(thisEnemy.enemyName == "Orange")
        {
            GameObject SceneObject_OrangeExplosion = Instantiate(orangeExplosionEffect, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
    public void DamageEffect(int damage)
    {
        
    }
    public void ActivateFlashEffect()
    {
        if (!flashEffect.activeInHierarchy)
        {
            flashEffect.SetActive(true);
            flashEffectTimer = 0.05f;
        }

    }
    public void FlashEffect()
    {
        if(flashEffectTimer > 0)
        {
            flashEffectTimer -= Time.deltaTime;
        }
        if(flashEffectTimer <= 0 && flashEffect.activeInHierarchy)
        {
            flashEffect.SetActive(false);
        }
    }
    public void HeartEffect()
    {
        if(thisEnemy.health < originalHealth * ((float)2/3) && hearts[2].activeInHierarchy)
        {
            hearts[2].SetActive(false);
            //GameObject SceneObject_HeartExplosion = Instantiate(pixelHeartExplosionEffect, hearts[2].transform.position, hearts[2].transform.rotation);
        }
        if (thisEnemy.health < originalHealth * ((float)1 / 3) && hearts[1].activeInHierarchy)
        {
            hearts[1].SetActive(false);
            //GameObject SceneObject_HeartExplosion = Instantiate(pixelHeartExplosionEffect, hearts[1].transform.position, hearts[1].transform.rotation);
        }
    }
}