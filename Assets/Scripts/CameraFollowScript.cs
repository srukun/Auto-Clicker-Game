using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public Transform player;
    void Start()
    {
        
    }

    void LateUpdate()
    {
        if(player != null)
        {
            transform.position = new Vector3(
                player.position.x,
                player.position.y,
                transform.position.z
            );
        }

    }
}
