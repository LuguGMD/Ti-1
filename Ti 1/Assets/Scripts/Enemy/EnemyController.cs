using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public int life = 1;
    //Chance to drop something when dead
    public int dropChance;
    //Things to drop when dead
    public GameObject[] dropTable;
    //Amount of points given when dead
    public float points = 0f;

    //Holds the component of the scene manager
    private StatsManager sm;


    // Start is called before the first frame update
    void Start()
    {
        GameObject manager = GameObject.Find("SceneManagement");
        sm = manager.GetComponent<StatsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            //Destroying the colliding bullet
            Destroy(other.gameObject);

            //Getting the damage from the hitted bullet
            int damage = other.GetComponent<BulletMovement>().damage;

            //Running damage method
            Damage(damage);
        }
    }

    void Damage(int damage)
    {
        //Taking damage
        life -= damage;

        //Checking if enemy is dead
        if(life <= 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        //Getting the chance to drop
        int chance = Random.Range(0, 100);

        //Checking if the chance has been hited
        if (chance < dropChance)
        {
            //Getting the index of the drop
            int drop = Random.Range(0, dropTable.Length);
            //Creating the drop
            Instantiate(dropTable[drop], transform.position, dropTable[drop].transform.rotation);
        }

        //Adding points to the manager
        sm.points += points;

        //Destroying object
        Destroy(gameObject);
    }

}
