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
    public Vector2 movement;
    void Start()
    {
        position = transform.position;
        hero = new Hero();
    }

    void FixedUpdate()
    {
        Movement();
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (movement.sqrMagnitude > 1)
        {
            movement.Normalize();
        }

        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);

    }

    public void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float speed = 3.5f;
        Vector3 moveDirection = new Vector3(horizontal, vertical, 0).normalized;

        Vector3 newPosition = transform.position + moveDirection * speed * Time.deltaTime;



        transform.position = newPosition;
    }




}
