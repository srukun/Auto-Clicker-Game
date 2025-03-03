using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 position;
    void Start()
    {
        position = transform.position;
    }

    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && position.x > -1.25)
        {
            position.x -= 1.25f;
            transform.position = new Vector3(position.x, position.y, -2);
        }
        else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && position.x < 1.25)
        {
            position.x += 1.25f;
            transform.position = new Vector3(position.x, position.y, -2);
        }
    }


}
