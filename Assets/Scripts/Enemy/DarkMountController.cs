using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkMountController : BaseEnemyController
{
    public GameObject returnHomePortal;
    public override void HandleAttack()
    {

    }
    public override  void HandleMovement()
    {
        //a
    }
    public override void Die()
    {
        if(returnHomePortal != null)
        {
            GameObject portal = Instantiate(returnHomePortal, new Vector3(0, 0, -1), Quaternion.identity);
        }
        base.Die();
    }
}
