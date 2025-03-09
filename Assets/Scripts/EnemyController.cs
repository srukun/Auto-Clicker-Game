using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
    public SceneManager sceneManager;
    public RoomManager roomManager;

    public GameObject SceneObj_Canvas;
    public GameObject enemyInformationTextPrefab;
    public GameObject SceneObj_EnemyInformationText;
    public int spawnCode;
    public GameObject heroSceneObject;
    public GameObject projectilePrefab;
    public float attackDelayTimer;
    Vector2 heroPosition;
    float distanceFromHero;
    void Start()
    {
        attackDelayTimer = 0.33f;
        originalHealth = thisEnemy.health;
        roomManager = sceneManager.roomManager;

    }

    void Update()
    {
        FlashEffect();
        CalculateHeroDistance();
        Movement();
        Attack();
        if(attackDelayTimer > 0)
        {
            attackDelayTimer -= Time.deltaTime;
        }
    }
    public void KillThisEnemy()
    {
        GameObject SceneObject_OrangeExplosion = Instantiate(enemyExplosionEffect, transform.position, transform.rotation);

        sceneManager.IncreaseEarnedGold(thisEnemy.goldRewarded);
        Destroy(gameObject);
        roomManager.DecreaseEnemiesRemaining();
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
        }
        if (thisEnemy.health < originalHealth * ((float)1 / 3) && hearts[1].activeInHierarchy)
        {
            hearts[1].SetActive(false);
        }
    }
    public void Attack()
    {
        if (heroSceneObject.GetComponent<HeroController>().hero.health > 0 && distanceFromHero < 3f && attackDelayTimer <= 0)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            projectile.GetComponent<ProjectileController>().Initialize(heroSceneObject.transform.position, 10f);

            attackDelayTimer = 0.75f;
        }
    }

    public void Movement()
    {
        if (distanceFromHero < 4.5f)
        {
            float xOffset = Random.Range(-0.5f, 0.5f);
            float yOffset = Random.Range(-0.5f, 0.5f);

            // Calculate new position with offset
            Vector3 targetPosition = new Vector3(heroPosition.x + xOffset, heroPosition.y + yOffset, -2);

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 1f * Time.deltaTime);

            transform.position = new Vector3(transform.position.x, transform.position.y, -2);
        }
    }

    public void CalculateHeroDistance()
    {
        heroPosition = heroSceneObject.transform.position;
        distanceFromHero = Vector2.Distance(heroPosition, transform.position);
        Debug.Log(distanceFromHero);
    }
}
