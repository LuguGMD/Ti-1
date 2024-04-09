using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DeskManager : MonoBehaviour
{
    //GameObject to be created as the floor
    public GameObject desk;

    private Vector3 deskDist = new Vector3(0f, 0f, 7.2f);
    private Vector3 deskPos = new Vector3(0f, 0f, 0f);

    private GameObject lastDesk;
    private MoveBackward mb;

    void Start()
    {
        //Getting the component of the prefab
        mb = desk.GetComponent<MoveBackward>();

        //Getting the spd of the desk
        mb.spd = 10f;

        //Creating the first desk
        lastDesk = Instantiate(desk, deskPos, desk.transform.rotation);

        

        //Creating other desks
        for (int i = 0; i < 10; i++)
        {
            CreateDesk();
        }

        float repeatTime = deskDist.z / mb.spd;

        InvokeRepeating("CreateDesk", 0f, repeatTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateDesk()
    {
        //Creating a new desk
        lastDesk = Instantiate(desk, lastDesk.transform.position + deskDist, desk.transform.rotation);
    }
}
