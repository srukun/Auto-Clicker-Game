using UnityEngine;

public class ToombloomController : BaseEnemyController
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 3f;
    public int seedCount = 10;

    public override void HandleMovement()
    {
        // Hover slowly back and forth
        float move = Mathf.Sin(Time.time * 0.5f) * 0.5f;
        transform.position += new Vector3(move, 0f, 0f) * Time.deltaTime;
    }

    public override void HandleAttack()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f && Vector2.Distance(transform.position, player.position) <= stats.detectionRange)
        {
            FireSeedStorm();
            attackTimer = stats.attackCooldown;
        }
    }

    private void FireSeedStorm()
    {
        for (int i = 0; i < seedCount; i++)
        {
            float angle = Random.Range(0f, 360f);
            Vector2 dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            GameObject bullet = Instantiate(bulletPrefab, transform.position + (Vector3)(dir * 0.1f), Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = dir * bulletSpeed;
        }
    }
}
