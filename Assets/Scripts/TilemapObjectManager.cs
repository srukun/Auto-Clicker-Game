using System.Collections;
using UnityEngine;

public class TilemapObjectManager : MonoBehaviour
{
    public Transform player;
    private SpriteRenderer spriteRenderer;
    private bool isFaded = false;
    public Color color;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        color = spriteRenderer.color;
    }

    void Update()
    {
        if(player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);
            Fade();
        }

    }
    public void Fade()
    {
        if (!isFaded && Vector2.Distance(transform.position, player.position) <= 2.5f)
        {
            color.a = 0.33f;
            spriteRenderer.color = color;
            isFaded = true;
        }
        else if(Vector2.Distance(transform.position, player.position) > 2.5f)
        {
            color.a = 1f;
            spriteRenderer.color = color;
            isFaded = false;
        }
    }

}
