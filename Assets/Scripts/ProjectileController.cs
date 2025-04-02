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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.transform.tag);
        if(collision.transform.tag == "Hero")
        {
            Hero hero = collision.gameObject.GetComponent<HeroController>().hero;
            collision.gameObject.GetComponent<HeroController>().healthbar.SetHealth(hero.health - 10, hero.maxHealth);
        }
    }

}
