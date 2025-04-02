using UnityEngine;

public class AshrootController : BaseEnemyController
{
    public GameObject bulletPrefab;
    public float projectileSpeed = 6f;

    private bool isFlurrying = false;
    private float flurryTimer = 0f;
    private float bulletInterval = 0.15f;
    private int bulletsRemaining = 0;
    private Vector2 flurryDirection;



    public override void HandleAttack()
    {
        if (!isFlurrying)
        {
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0f && Vector2.Distance(transform.position, player.position) <= stats.detectionRange)
            {
                StartFlurry(player.position);
            }
        }
        else
        {
            flurryTimer -= Time.deltaTime;

            if (flurryTimer <= 0f && bulletsRemaining > 0)
            {
                FireSingleFlurryBullet();
                flurryTimer = bulletInterval;
                bulletsRemaining--;

                if (bulletsRemaining <= 0)
                {
                    isFlurrying = false;
                    attackTimer = stats.attackCooldown;
                }
            }
        }
    }

    private void StartFlurry(Vector2 targetPosition)
    {
        flurryDirection = (targetPosition - (Vector2)transform.position).normalized;
        bulletsRemaining = Random.Range(7, 12);
        flurryTimer = 0f;
        isFlurrying = true;
    }

    private void FireSingleFlurryBullet()
    {
        float baseAngle = Mathf.Atan2(flurryDirection.y, flurryDirection.x) * Mathf.Rad2Deg;
        float angleOffset = Random.Range(-20f, 20f);
        float finalAngle = baseAngle + angleOffset;
        Vector2 offsetDirection = new Vector2(Mathf.Cos(finalAngle * Mathf.Deg2Rad), Mathf.Sin(finalAngle * Mathf.Deg2Rad));

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = offsetDirection.normalized * projectileSpeed;
    }
}
