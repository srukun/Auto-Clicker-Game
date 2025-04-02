using UnityEngine;
using System.Collections;

public class PetalbiteController : BaseEnemyController
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float windupTime = 0.3f;
    private bool isWindingUp = false;

    public override void HandleMovement()
    {
        if (!isWindingUp)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)(direction * stats.moveSpeed * Time.deltaTime);
        }
    }

    public override void HandleAttack()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f && !isWindingUp && Vector2.Distance(transform.position, player.position) <= stats.detectionRange)
        {
            StartCoroutine(StingerShot());
            attackTimer = stats.attackCooldown;
        }
    }

    private IEnumerator StingerShot()
    {
        isWindingUp = true;
        yield return new WaitForSeconds(windupTime);

        Vector2 direction = (player.position - transform.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, transform.position + (Vector3)(direction * 0.2f), Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

        isWindingUp = false;
    }
}
