using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pen : EnemyController
{
    private float dist;
    public float minDist;

    private bool isNear = false;

    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

        if (!isNear)
        {
            //Getting the distance between enemy and player
            dist = (transform.position - CameraController.main.transform.position).magnitude;

            //Moving towards player
            transform.position += transform.forward * speed * Time.deltaTime;

            //Checking if enemy is near enough
            if (dist <= minDist)
            {
                isNear = true;
            }
        }
        else
        {
            //Shooting
            shooter.Shoot();
        }
    }
}
