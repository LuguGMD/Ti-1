using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public BulletScriptable bulletStats;

    private float speed;
    public int damage;

    private HealthSystem hs;

    // Start is called before the first frame update
    void Start()
    {
        //Destroying self after 3 seconds
        Destroy(gameObject, 3f);

        hs = GetComponent<HealthSystem>();
        StartStats();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        //Checking it the bullet is dead
        if(hs.isDead)
        {
            //Destroying self
            Destroy(gameObject);
        }
    }

    void StartStats()
    {
        //Changing stats
        speed = bulletStats.speed;
        damage = bulletStats.damage;

        //Changing Mesh
        GetComponent<MeshFilter>().mesh = bulletStats.mesh;

        //Changing tag
        tag = bulletStats.tag;
    }

}
