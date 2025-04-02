using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    public Vector3 position;
    public Hero hero;

    public GameObject sceneManagerObject;
    public Camera camera;
    public Animator animator;

    public float speed = 3.5f;
    public Vector2 movement;
    public Rigidbody2D rb;
    void Start()
    {
        position = transform.position;
        hero = new Hero();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.sqrMagnitude > 1)
        {
            movement.Normalize();
        }

        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);

        MoveCharacter();
    }

    void MoveCharacter()
    {
        /*        Vector3 moveDirection = new Vector3(movement.x, movement.y, 0);
                transform.position += moveDirection * speed * Time.deltaTime;*/
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

    }
}
