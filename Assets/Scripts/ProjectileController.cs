using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 direction;
    private float speed = 4f;
    private float travelDistance;
    private float distanceTraveled = 0f;
    public float timer = 3f;

    public void Initialize(Vector3 targetPosition, float maxDistance)
    {
        startPosition = transform.position;
        travelDistance = maxDistance;

        direction = (targetPosition - startPosition).normalized;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        Vector3 movement = direction * speed * Time.deltaTime;
        transform.position += movement;

        distanceTraveled += movement.magnitude;

        if (distanceTraveled >= travelDistance || timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
