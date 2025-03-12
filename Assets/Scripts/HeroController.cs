using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    public Vector3 position;
    public Hero hero;

    public GameObject sceneManagerObject;

    void Start()
    {
        position = transform.position;
        hero = new Hero();
    }

    void Update()
    {
        Movement();
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
