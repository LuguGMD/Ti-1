using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{

    //Amount of points to distribute in spawns
    public float enemyPoints;

    //Enemy to be instantiated
    public GameObject[] enemy;
    //Cost of the enemy to be instantiated
    public int[] cost;

    //Start and end position to enemies to be spawned
    [SerializeField] Transform spawnStart;
    [SerializeField] Transform spawnEnd;

    public float[] spawnCooldown;
    public float spawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemySpawner());
    }

    // Update is called once per frame
    void Update()
    {
        

        GameEnd();
    }

    void SpawnEnemy()
    {
        //Getting the index of the enemy
        int index = Random.Range(0, enemy.Length);

        //Checking if there are enough points to spawn an Enemy
        if (enemyPoints >= cost[index])
        {
            //Getting a random position for the enemy
            Vector3 spawnPos = new Vector3(Random.Range(spawnStart.position.x, spawnEnd.position.x), 0f, spawnStart.position.z);

            //Creating enemy
            Instantiate(enemy[index], spawnPos, enemy[index].transform.rotation);

            //Reducing points by cost
            enemyPoints -= cost[index];

            spawnTimer = Random.Range(spawnCooldown[0], spawnCooldown[1]);
        }
    }

    void GameEnd()
    {
        Debug.Log(enemyPoints);

        //Checking if the manager has run out of points
        if (enemyPoints <= 0)
        {
            //Checking if there is a Enemy alive
            if (!GameObject.FindGameObjectWithTag("Enemy"))
            {
                //Going to the Win Screen
                SceneManager.LoadScene("WinScreen");
            }
        }
    }

    IEnumerator EnemySpawner()
    {
        while (MainManager.instance.isGameRunning)
        {
            yield return new WaitForSeconds(spawnTimer);
            SpawnEnemy();
        }
    }

}
