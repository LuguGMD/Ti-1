using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{

    public float spd;

    private float boundary = 45f;

    public int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Moving bullet forward
        transform.position += Vector3.forward * spd * Time.deltaTime;

        //Checking if bullet is out of boundaries
        if(transform.position.z > boundary || transform.position.z < -15f)
        {
            //Destroying self
            Destroy(gameObject);
        }
    }
}
