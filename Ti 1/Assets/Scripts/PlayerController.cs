using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movement Variables
    public float spd;
    private float horizontalInput;
    private float verticalInput;

    private float boundaryX = 4.8f;
    private float boundaryZMin = -8.5f;
    private float boundaryZMax = 10f;

    public int maxLife = 3;
    public int life;

    // Start is called before the first frame update
    void Start()
    {
        //Starting life
        life = maxLife;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        //DEBUG


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Heal"))
        {
            //Destroying collectable
            Destroy(other.gameObject);

            //Healing player
            life++;

            //Clamping life
            if(life > maxLife)
            {
                life = maxLife;
            }
        }

        if(other.CompareTag("Enemy"))
        {   
            //Destroying enemy
            Destroy(other.gameObject);

            //Running damage method
            Damage(1);
        }
    }

    void Movement()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //Getting speed
        float h = horizontalInput * spd;
        float v = verticalInput * spd;

        //Getting movement
        Vector3 movement = new Vector3(h, 0f, v);

        //Normalizing direction
        movement.Normalize();

        //Applying speed and normalizing it
        movement *= spd * Time.deltaTime;

        //Moving player
        transform.position += movement;

        //Checking X boundaries
        if(transform.position.x > boundaryX)
        {
            transform.position = new Vector3(boundaryX, transform.position.y, transform.position.z);
        }
        else if(transform.position.x < -boundaryX)
        {
            transform.position = new Vector3(-boundaryX, transform.position.y, transform.position.z);
        }
        //Checking Z boundaries
        if (transform.position.z > boundaryZMax)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, boundaryZMax);
        }
        else if (transform.position.z < boundaryZMin)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, boundaryZMin);
        }

    }

    void Damage(int damage)
    {
        //Taking the damage
        life -= damage;

        //Checking if player is dead
        if(life <= 0) 
        {
            //Clamping life
            life = 0;

        }
    }

}
