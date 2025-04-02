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

}
