using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public BulletScriptable bulletStats;

    private float speed;
    private int damage;

    // Start is called before the first frame update
    void Start()
    {
        StartStats();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void StartStats()
    {
        speed = bulletStats.speed;
        damage = bulletStats.damage;

        GetComponent<MeshFilter>().mesh = bulletStats.mesh;
    }

}
