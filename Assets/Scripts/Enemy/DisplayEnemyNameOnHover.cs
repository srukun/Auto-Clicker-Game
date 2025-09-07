using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class DisplayEnemyNameOnHover : MonoBehaviour
{
    public GameObject enemyName;
    public float activeTime = 0;
    void Start()
    {
        
    }

    void Update()
    {


        if (activeTime > 0) { 
            enemyName.SetActive(true);
            activeTime -= Time.deltaTime;
        }
        if(activeTime <= 0 && enemyName.activeInHierarchy)
        {
            enemyName.SetActive(false);
        }
    }
    void OnMouseOver()
    {
        if (activeTime <= 0)
        {
            activeTime = 2.5f;
        }
    }
}
