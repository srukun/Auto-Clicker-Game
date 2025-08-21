using UnityEngine;

public class FadewiltController : EnemyController
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 3f;
    public int bulletCount = 12;

    public override void HandleMovement()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)(direction * (stats.moveSpeed * 0.4f) * Time.deltaTime);
    }

    public void HandleAttack()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f && Vector2.Distance(transform.position, player.position) <= stats.detectionRange)
        {
            FireCircularBurst();
            attackTimer = stats.attackCooldown;
        }
    }

    private void FireCircularBurst()
    {
        float angleStep = 360f / bulletCount;
        float angle = 0f;

        for (int i = 0; i < bulletCount; i++)
        {
            float rad = angle * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
            Vector3 spawnPos = transform.position + (Vector3)(dir * 0.2f);

            GameObject bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = dir * bulletSpeed;

            angle += angleStep;
        }
    }
}
