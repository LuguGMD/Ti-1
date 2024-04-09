using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectiles : MonoBehaviour
{
    
    private int bulletSlot = 0;
    public GameObject[] bulletsPrefabs;

    [SerializeField] GameObject tip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Getting inputs
        int left = Input.GetKeyDown(KeyCode.Q) ? 1 : 0;
        int right = Input.GetKeyDown(KeyCode.E) ? 1 : 0;

        //Changing the slot
        bulletSlot += right - left;

        //Clamping bulletSlot
        if(bulletSlot < 0) 
        {
            bulletSlot = bulletsPrefabs.Length - 1;
        }
        else if(bulletSlot >= bulletsPrefabs.Length)
        {
            bulletSlot = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Getting the prefab to create
            GameObject bullet = bulletsPrefabs[bulletSlot];
            //Creating bullet
            Instantiate(bullet, tip.transform.position, bullet.transform.rotation);
        }
    }
}
