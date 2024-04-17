using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{

    GameObject player;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = (player.transform.position - transform.position).normalized;

        transform.position += dir * speed * Time.deltaTime;
    }
}
