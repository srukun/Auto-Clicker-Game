using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    public RoomManager roomManager;
    public SceneManager sceneManager;
    public string direction;


    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2Int newPosition = sceneManager.currentPosition; // Store current position

        if (direction == "top")
        {
            newPosition.x -= 1; 
        }
        else if (direction == "bottom")
        {
            newPosition.x += 1; 
        }
        else if (direction == "left")
        {
            newPosition.y -= 1; 
        }
        else if (direction == "right")
        {
            newPosition.y += 1;
        }

        sceneManager.currentPosition = newPosition; // Apply new position
        roomManager.RoomSetUp();
        roomManager.sceneManager.HideMinimap();
    }


}
