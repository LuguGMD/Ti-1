using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scissor : EnemyController
{
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
