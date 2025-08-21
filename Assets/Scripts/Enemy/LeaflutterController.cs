using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaflutterController : EnemyController
{
    public GameObject portalPrefab;
    public GameObject projectilePrefab;

    private enum Phase { Phase1, Phase2, Phase3, Phase4 }
    private Phase currentPhase = Phase.Phase1;

    private float spiralAngle = 0f;
    private float spiralInterval = 0.15f;

    private Vector3 originalPosition;

    public override void Start()
    {
        base.Start();
        canPatrol = false;
        originalPosition = transform.position;

    }

    public override void Update()
    {
        base.FlashEffect();

        float healthPercent = currentHealth / stats.maxHealth;

        if (healthPercent > 0.75f)
        {
            currentPhase = Phase.Phase1;
        }
        else if (healthPercent > 0.5f)
        {
            currentPhase = Phase.Phase2;
        }
        else if (healthPercent > 0.125f)
        {
            currentPhase = Phase.Phase3;
        }
        else
        {
            currentPhase = Phase.Phase4;
        }

        if (initialAgroDelay > 0f)
        {
            initialAgroDelay -= Time.deltaTime;
            return;
        }

        HandleAttack();
        HandleMovement();
    }

    public void HandleAttack()
    {
        if (currentPhase == Phase.Phase1)
        {
            HandleTargetedShots();
        }
        else if (currentPhase == Phase.Phase2)
        {
            HandleRadialShots();
        }
        else if (currentPhase == Phase.Phase3)
        {
            HandleSpiralBarrage();
        }
        else if (currentPhase == Phase.Phase4)
        {
            HandleRadialShots();
        }
    }

    public void HandleTargetedShots()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0f)
        {
            int shots = Random.Range(2, 4);
            for (int i = 0; i < shots; i++)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                proj.GetComponent<Rigidbody2D>().velocity = direction * 4f;
            }
            attackTimer = stats.attackCooldown;
        }
    }

    public void HandleRadialShots()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0f)
        {
            int bulletCount = 12;
            for (int i = 0; i < bulletCount; i++)
            {
                float angle = i * (360f / bulletCount);
                Vector2 dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
                GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                proj.GetComponent<Rigidbody2D>().velocity = dir * 4f;
            }
            attackTimer = stats.attackCooldown;
        }
    }

    public void HandleSpiralBarrage()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0f)
        {
            float angle = spiralAngle;
            Vector2 dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            GameObject bullet = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = dir * 5f;

            spiralAngle += 15f;
            if (spiralAngle >= 360f)
            {
                spiralAngle -= 360f;
            }

            attackTimer = spiralInterval;
        }
    }

    public override void HandleMovement()
    {
        if (currentPhase == Phase.Phase1 || currentPhase == Phase.Phase4)
        {
            base.HandleMovement();
        }
        else if (currentPhase == Phase.Phase2)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, stats.moveSpeed * Time.deltaTime);
        }
    }


    public override void Die()
    {
        if (portalPrefab != null)
        {
            GameObject portal = Instantiate(portalPrefab, new Vector3(0, 0, -1), Quaternion.identity);
            PortalManager portalManager = portal.GetComponent<PortalManager>();
            portalManager.direction = "home";
        }
        base.Die();
    }
}
