using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceManager : MonoBehaviour
{
    public MapManager mapManager;

    //entrances and walls
    public GameObject topWall;
    public GameObject topEntrance;
    public GameObject bottomWall;
    public GameObject bottomEntrance;
    public GameObject leftWall;
    public GameObject leftEntrance;
    public GameObject rightWall;
    public GameObject rightEntrance;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void OpenEntrance()
    {
        if(mapManager.currentRoom.top != null)
        {
            topWall.SetActive(false);
            topEntrance.SetActive(true);
        }
    }
    public void LockEntrance()
    {
        if (mapManager.currentRoom.top != null)
        {
            topWall.SetActive(true);
            topEntrance.SetActive(false);
        }
    }
}
