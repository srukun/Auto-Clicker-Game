using UnityEngine;

public class HeroProjectileController : MonoBehaviour
{
    public float lifetime = 10f;
    private float timer;

    void Start()
    {
        timer = lifetime;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            collision.transform.GetComponent<EnemyController>().TakeDamage(5);
            Destroy(gameObject);
        }
    }
}
