using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    public RoomManager roomManager;
    public SceneManager sceneManager;
    public string direction;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hero"))
        {
            StartCoroutine(sceneManager.transitionManager.SlideTransition(MoveHero));
        }
    }

    private void MoveHero()
    {
        Vector2Int newPosition = sceneManager.currentPosition;
        GameObject hero = sceneManager.heroGameObject;

        if (direction == "top")
        {
            newPosition.x -= 1;
            hero.transform.position = new Vector3(0, -6.5f, -10);
        }
        else if (direction == "bottom")
        {
            newPosition.x += 1;
            hero.transform.position = new Vector3(0, 6.5f, -10);
        }
        else if (direction == "left")
        {
            newPosition.y -= 1;
            hero.transform.position = new Vector3(6.5f, 0, -10);
        }
        else if (direction == "right")
        {
            newPosition.y += 1;
            hero.transform.position = new Vector3(-6.5f, 0, -10);
        }

        sceneManager.currentPosition = newPosition;
        roomManager.RoomSetUp();
        roomManager.sceneManager.HideMinimap();
    }
}
