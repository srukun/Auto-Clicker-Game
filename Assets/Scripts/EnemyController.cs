using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyClass thisEnemy;
    public GameObject[] hearts;
    public int originalHealth;
    public GameObject pixelHeartExplosionEffect;
    public GameObject enemyExplosionEffect;
    public GameObject flashEffect;
    public float flashEffectTimer;
    public GameObject SceneObject_ArenaManager;

    public GameObject SceneObj_Canvas;
    public GameObject enemyInformationTextPrefab;
    public GameObject SceneObj_EnemyInformationText;
    public int spawnCode;
    public GameObject SceneObject_Hero;
    public GameObject Prefab_Projectile;
    void Start()
    {
        SceneObj_EnemyInformationText = Instantiate(enemyInformationTextPrefab);
        SceneObj_EnemyInformationText.transform.SetParent(SceneObj_Canvas.transform, false);
        if (spawnCode == 1)
        {
            SceneObj_EnemyInformationText.transform.localPosition = new Vector3(0, 35, 0);
            SceneObj_EnemyInformationText.GetComponent<TextMeshProUGUI>().SetText(thisEnemy.enemyName + "\nLv. " + thisEnemy.level);
        }
        else if (spawnCode == 0)
        {
            SceneObj_EnemyInformationText.transform.localPosition = new Vector3(-115, 35, 0);
            SceneObj_EnemyInformationText.GetComponent<TextMeshProUGUI>().SetText(thisEnemy.enemyName + "\nLv. " + thisEnemy.level);
        }
        else if (spawnCode == 2)
        {
            SceneObj_EnemyInformationText.transform.localPosition = new Vector3(115, 35, 0);
            SceneObj_EnemyInformationText.GetComponent<TextMeshProUGUI>().SetText(thisEnemy.enemyName + "\nLv. " + thisEnemy.level);
        }
        originalHealth = thisEnemy.health; 
    }

    void Update()
    {
        FlashEffect();
    }
    public void KillThisEnemy()
    {
        GameObject SceneObject_OrangeExplosion = Instantiate(enemyExplosionEffect, transform.position, transform.rotation);

        SceneObject_ArenaManager.GetComponent<ArenaManager>().IncreaseEarnedGold(thisEnemy.goldRewarded);
        SceneObject_ArenaManager.GetComponent<ArenaManager>().enemiesRemaining--;
        if(SceneObject_ArenaManager.GetComponent<ArenaManager>().enemiesRemaining <= 0)
        {
            SceneObject_ArenaManager.GetComponent<ArenaManager>().signalActive = true;
        }
        Destroy(SceneObj_EnemyInformationText);
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
            flashEffectTimer = 0.07f;
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
    public void Attack()
    {
        if(SceneObject_Hero.GetComponent<HeroController>().hero.health > 0)
        {

        }
    }
}
