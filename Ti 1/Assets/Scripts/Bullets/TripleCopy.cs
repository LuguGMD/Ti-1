using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleCopy : MonoBehaviour
{
    public float speed;
    public Color color;
    public int damage;

    public ParticleSystem psPaint;
    public HealthSystem hs;

    // Start is called before the first frame update
    void Start()
    { 
        hs = GetComponent<HealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Manager.main.isGameRunning)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        //Checking it the bullet is dead
        if (hs.isDead)
        {
            psPaint.startColor = color;
            //Creating the paint particle system at the bullet
            Instantiate(psPaint, transform.position, transform.rotation);
            //Destroying self
            Destroy(gameObject);
        }
    }
}
