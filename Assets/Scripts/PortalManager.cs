using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PortalManager : MonoBehaviour
{
    public RoomManager roomManager;
    public SceneManager sceneManager;
    public RoomNode currentNode;
    public string direction;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Hero" && direction != "home")
        {
            StartCoroutine(sceneManager.transitionManager.SlideTransition(MoveHero));
        }
        else if (collision.transform.tag == "Hero" && direction == "home")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
    private void MoveHero()
    {
        GameObject hero = sceneManager.heroGameObject;

        if (direction == "top")
        {
            hero.transform.position = new Vector3(0, -6.5f, -2);
            roomManager.currentNode = roomManager.currentNode.top;
        }
        else if (direction == "bottom")
        {
            hero.transform.position = new Vector3(0, 6.5f, -2);
            roomManager.currentNode = roomManager.currentNode.bottom;
        }
        else if (direction == "left")
        {
            hero.transform.position = new Vector3(6.5f, 0, -2);
            roomManager.currentNode = roomManager.currentNode.left;
        }
        else if (direction == "right")
        {
            hero.transform.position = new Vector3(-6.5f, 0, -2);
            roomManager.currentNode = roomManager.currentNode.right;
        }

        //sceneManager.currentPosition = newPosition;
        roomManager.InitializeRoom();
        //roomManager.sceneManager.SetupMinimap();
    }
}
