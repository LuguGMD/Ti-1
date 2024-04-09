using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackward : MonoBehaviour
{

    public float spd;
    //Boundarie for the desk to be deleted
    private float boundary = -15f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Moving the desk
        transform.position += Vector3.back * spd * Time.deltaTime;

        //Checking if the desk has past the boundarie
        if(transform.position.z <= boundary)
        {
            Destroy(gameObject);
        }
    }
}
