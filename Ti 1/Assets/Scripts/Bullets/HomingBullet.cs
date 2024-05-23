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
        if (Manager.main.isGameRunning)
        {
            
            if (hasTarget)
            {
                if (target != null)
                {

                    //Getting the direction towards the enemy
                    Vector3 dir = (target.transform.position - transform.position).normalized;

                    transform.position += speed / 1.2f * dir * Time.deltaTime;
                }
                else
                {
                    hasTarget = false;
                    FindTarget();
                }
            }
            else
            {
                //Trying to find a target
                FindTarget();
                transform.position += transform.forward * speed * Time.deltaTime;
            }
        }


        //Checking it the bullet is dead
        if (hs.isDead)
        {
            psPaint.startColor = color;
            //Creating the paint particle system at the bullet
            Instantiate(psPaint, transform.position, transform.rotation);
            //Destroying self
            Destroy(gameObject);
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

        if(target != null) hasTarget = true;

    }

}
