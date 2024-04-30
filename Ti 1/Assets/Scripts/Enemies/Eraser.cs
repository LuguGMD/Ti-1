using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eraser : EnemyController
{
    public override void Start()
    {
        base.Start();

        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Manager.main.isGameRunning)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }
}
