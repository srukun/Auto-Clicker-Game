using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeProjectileScript : MonoBehaviour
{
    public KnifeClass thisKnife;
    public float lifetime;
    void Start()
    {
        lifetime = 5f;
    }

    
    void Update()
    {
        if(lifetime > 0)
        {
            lifetime -= Time.deltaTime;
        }
        DestroyAfterDistance();
    }
    public void DestroyAfterDistance()
    {
        if(lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyController enemy = collision.GetComponent<EnemyController>();
            if (enemy != null)
            {
                int damage = thisKnife.getDamage();
                damage = 10;
                enemy.TakeDamage(damage);

                enemy.ActivateFlashEffect();

                Destroy(gameObject);
            }
        }
    }


}
