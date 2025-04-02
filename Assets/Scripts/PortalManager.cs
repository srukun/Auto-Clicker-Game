using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PortalManager : MonoBehaviour
{
    public RoomManager roomManager;
    public SceneManager sceneManager;
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
        Vector2Int newPosition = sceneManager.currentPosition;
        GameObject hero = sceneManager.heroGameObject;

        if (direction == "top")
        {
            newPosition.x -= 1;
            hero.transform.position = new Vector3(0, -6.5f, -2);
        }
        else if (direction == "bottom")
        {
            newPosition.x += 1;
            hero.transform.position = new Vector3(0, 6.5f, -2);
        }
        else if (direction == "left")
        {
            newPosition.y -= 1;
            hero.transform.position = new Vector3(6.5f, 0, -2);
        }
        else if (direction == "right")
        {
            newPosition.y += 1;
            hero.transform.position = new Vector3(-6.5f, 0, -2);
        }

        sceneManager.currentPosition = newPosition;
        roomManager.RoomSetUp();
        roomManager.sceneManager.SetupMinimap();
    }
}
