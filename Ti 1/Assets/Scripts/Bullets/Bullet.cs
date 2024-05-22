using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public BulletScriptable bulletStats;

    protected float speed;
    public int damage;

    protected Color color;

    protected HealthSystem hs;

    [SerializeField] private ParticleSystem psPaint;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //Destroying self after 3 seconds
        Destroy(gameObject, 3f);

        hs = GetComponent<HealthSystem>();
        StartStats();
    }

    

    // Update is called once per frame
    protected virtual void Update()
    {
        if (Manager.main.isGameRunning)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        //Checking it the bullet is dead
        if(hs.isDead)
        {
            psPaint.startColor = color;
            //Creating the paint particle system at the bullet
            Instantiate(psPaint, transform.position, transform.rotation);
            //Destroying self
            Destroy(gameObject);
        }
    }

    public void StartStats()
    {
        //Changing stats
        speed = bulletStats.speed;
        damage = bulletStats.damage;
        color = bulletStats.color;

        //Changing material
        var materialsCopy = GetComponent<MeshRenderer>().materials;
        materialsCopy[0] = bulletStats.material;
        GetComponent<MeshRenderer>().materials = materialsCopy;

        //Changing tag
        tag = bulletStats.tag;
    }

}
