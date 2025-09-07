using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PortalManager : MonoBehaviour
{
    public EntranceManager entranceManager;
    public SceneManager sceneManager;
    public MapNode currentNode;
    public string direction;
    public MapManager mapManager;

    private void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
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
            hero.transform.position = new Vector3(0, -8f, -2);
            mapManager.currentRoom = mapManager.currentRoom.top;
        }

        entranceManager.LockEntrance();
        mapManager.ChangeRoom(mapManager.currentRoom.top);
    }
}
