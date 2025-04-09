using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    public EnemyStats stats;
    public Transform player;
    public float currentHealth;
    public float attackTimer;

    public bool canPatrol;
    public float patrolRadius = 2.5f;
    public float patrolSpeed = 2f;
    public float patrolPauseTime = 1f;

    public Vector3 patrolOrigin;
    public Vector3 currentPatrolTarget;
    public bool isPatrolling = true;
    public bool isWaiting = false;
    public float patrolWaitTimer = 0f;

    public GameObject enemyExplosionEffect;
    public GameObject flashEffect;
    public float flashEffectTimer;

    public SceneManager sceneManager;
    public RoomManager roomManager;
    public Vector2 moveOffset;
    public Healthbar healthBar;

    public GameObject damagePopupPrefab;
    public float initialAgroDelay = 1.15f;
    public virtual void Start()
    {
        player = GameObject.FindWithTag("Hero").transform;
        currentHealth = stats.maxHealth;
        patrolOrigin = transform.position;
        PickNewPatrolTarget();
        float angle = Random.Range(0f, 360f);
        float radius = Random.Range(0.5f, 1.5f);
        moveOffset = new Vector2(
            Mathf.Cos(angle * Mathf.Deg2Rad),
            Mathf.Sin(angle * Mathf.Deg2Rad)
        ) * radius;
        canPatrol = true;
    }

    public virtual void Update()
    {
        float playerDistance = Vector2.Distance(transform.position, player.position);
        if(initialAgroDelay > 0f)
        {
            initialAgroDelay -= Time.deltaTime;
        }
        if (playerDistance <= stats.detectionRange && initialAgroDelay <= 0f)
        {
            isPatrolling = false;
            HandleMovement();
            HandleAttack();
        }
        else
        {
            isPatrolling = true;
            Patrol();
        }

        FlashEffect();

    }

    public virtual void HandleMovement()
    {
        if (player == null || stats == null) {
            return;
        } 

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance >= 1.5f)
        {
            Vector2 targetPosition = (Vector2)player.position + moveOffset;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, stats.moveSpeed * Time.deltaTime);
        }
    }
    public abstract void HandleAttack();

    public virtual void TakeDamage(float amount)
    {
        currentHealth -= amount;
        ActivateFlashEffect();

        if (currentHealth <= 0)
        {
            Die();
        }
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth, stats.maxHealth);
        }
        if (damagePopupPrefab != null)
        {
            Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-0.25f, 0.26f), 1f, 0);
            GameObject popup = Instantiate(damagePopupPrefab, spawnPosition, Quaternion.identity);
            popup.GetComponent<DamagePopup>().Setup(Mathf.RoundToInt(amount));
        }

    }

    public virtual void Die()
    {
        if (enemyExplosionEffect != null)
        {
            Instantiate(enemyExplosionEffect, transform.position, Quaternion.identity);
        }

        if (sceneManager != null)
        {
            sceneManager.IncreaseEarnedGold(stats.goldRewarded);
        }

        if (roomManager != null)
        {
            roomManager.ReduceEnemyCountOnKill();
        }

        Destroy(gameObject);
    }

    public void ActivateFlashEffect()
    {
        if (flashEffect != null && !flashEffect.activeInHierarchy)
        {
            flashEffect.SetActive(true);
            flashEffectTimer = 0.05f;
        }
    }

    public void FlashEffect()
    {
        if (flashEffect == null) return;

        if (flashEffectTimer > 0)
        {
            flashEffectTimer -= Time.deltaTime;
        }
        if (flashEffectTimer <= 0 && flashEffect.activeInHierarchy)
        {
            flashEffect.SetActive(false);
        }
    }

    public void Patrol()
    {
        if (canPatrol)
        {
            if (isWaiting)
            {
                patrolWaitTimer -= Time.deltaTime;
                if (patrolWaitTimer <= 0f)
                {
                    PickNewPatrolTarget();
                    isWaiting = false;
                }
                return;
            }

            float dist = Vector2.Distance(transform.position, currentPatrolTarget);
            if (dist < 0.1f)
            {
                isWaiting = true;
                patrolWaitTimer = patrolPauseTime;
            }
            else
            {
                Vector2 dir = (currentPatrolTarget - transform.position).normalized;
                transform.position += (Vector3)(dir * patrolSpeed * Time.deltaTime);
            }
        }

    }

    public void PickNewPatrolTarget()
    {
        Vector2 randomOffset = Random.insideUnitCircle * patrolRadius;
        currentPatrolTarget = patrolOrigin + new Vector3(randomOffset.x, randomOffset.y, 0f);
    }
}
