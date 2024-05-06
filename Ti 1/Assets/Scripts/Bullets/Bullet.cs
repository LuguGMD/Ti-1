using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public BulletScriptable bulletStats;

    private float speed;
    public int damage;

    //private Color color;

    private HealthSystem hs;

    [SerializeField] private ParticleSystem psPaint;

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
        if (Manager.main.isGameRunning)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        //Checking it the bullet is dead
        if(hs.isDead)
        {
            //Creating the paint particle system at the bullet
            Instantiate(psPaint, transform.position, transform.rotation);
            //Destroying self
            Destroy(gameObject);
        }
    }

    void StartStats()
    {
        //Changing stats
        speed = bulletStats.speed;
        damage = bulletStats.damage;
        //color = bulletStats.color;

        //Changing Mesh
        GetComponent<MeshFilter>().mesh = bulletStats.mesh;

        //Changing tag
        tag = bulletStats.tag;
    }

}
