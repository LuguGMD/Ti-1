using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HomingBullet : Bullet
{

    bool hasTarget = false;
    GameObject target;

    float closer = -1;

    protected override void Update()
    {
        base.Update();

        if (hasTarget )
        {

        }
        else
        {
            //Trying to find a target
            FindTarget();
        }

    }

    void FindTarget()
    {
        //Getting all enemies from the room
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            //Getting the distante to the enemy
            float dist = (transform.position - enemies[i].transform.position).magnitude;
            //Storing first value
            if (closer == -1)
            {
                target = enemies[i];
                closer = dist;
            }
            //Checking if it's closer
            if (dist < closer)
            {
                target = enemies[i];
                closer = dist;
            }

        }

    }

}
