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
    public void UpdateEntrances()
    {
        if(mapManager.currentRoom.top != null)
        {
            topWall.SetActive(false);
            topEntrance.SetActive(true);
        }
        else
        {
            topWall.SetActive(true);
            topEntrance.SetActive(false);
        }
        if(mapManager.currentRoom.bottom != null)
        {
            bottomWall.SetActive(false);
            bottomEntrance.SetActive(true);
        }
        else
        {
            bottomWall.SetActive(true);
            bottomEntrance.SetActive(false);
        }


    }
}
