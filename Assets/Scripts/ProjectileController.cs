using UnityEngine;

public class ProjectileController : MonoBehaviour
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
        if (collision.transform.tag == "Hero")
        {
            Hero hero = collision.gameObject.GetComponent<HeroController>().hero;
            hero.health -= 10;
            collision.gameObject.GetComponent<HeroController>().healthbar.SetHealth(hero.health, hero.maxHealth);
        }
        Destroy(gameObject);
    }
}
