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
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyController>().thisEnemy.ModifyHealth(thisKnife.getDamage());
            collision.gameObject.GetComponent<EnemyController>().ActivateFlashEffect();
            collision.gameObject.GetComponent<EnemyController>().HeartEffect();
            collision.gameObject.GetComponent<EnemyController>().DamageEffect(thisKnife.getDamage());
            if(collision.gameObject.GetComponent<EnemyController>().thisEnemy.health <= 0)
            {
                collision.gameObject.GetComponent<EnemyController>().KillThisEnemy();
            }
            Destroy(gameObject);
        }
    }
}
