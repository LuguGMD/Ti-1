using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Scissor : EnemyController
{

    public override void Start()
    {
        base.Start();

        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
