using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    public float spd;

    //Boundarie for stop moving
    public float boundary;

    //Bullet prefab
    public GameObject bullet;
    //Where the bullets come out from
    public Transform tip;

    //Cooldown between projectiles
    public float cooldown;
    //Timer until next projectile
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        //Randomizing stop position
        boundary += Random.Range(-3f,1f);
    }

    // Update is called once per frame
    void Update()
    {
        //Checking if the enemy is behind the boundarie
        if (transform.position.z >= boundary)
        {
            //Moving the desk
            transform.position += Vector3.back * spd * Time.deltaTime;
        }
        //Reached target position
        else
        {
            //Checking if enough time has passed
            if(timer <= 0)
            {
                //Reseting timer
                timer = cooldown;

                //Creating bullet
                Instantiate(bullet, tip.position, bullet.transform.rotation);
            }
            //Passing time
            timer -= 60 * Time.deltaTime;
        }
    }
}
