using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public EnemyScriptable enemyStats;
    private Shooter shooter;
    private HealthSystem hs;
    private DropSystem ds;

    protected float speed;

    private bool isShooter = false;

    public int damage = 1;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        StartStats();
    }

    // Update is called once per frame
    void Update()
    {
        if (isShooter)
        {
            shooter.Shoot();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("BulletPlayer"))
        {
            bool dead = hs.TakeDamage(other.GetComponent<Bullet>().damage);    
            if(dead)
            {
                KillEnemy();
            }
        }
    }

    private void OnDestroy()
    {
        Actions.enemyKilled?.Invoke();
    }

    public void KillEnemy()
    {
        ds.Drop(transform.position);
        Destroy(gameObject);
    }

    void StartStats()
    {
        ds = GetComponent<DropSystem>();

        ds.dropPrefabs = new GameObject[enemyStats.dropPrefabs.Length];
        ds.dropChances = new float[enemyStats.dropPrefabs.Length];

        for(int i = 0; i < enemyStats.dropPrefabs.Length; i++)
        {
            ds.dropPrefabs[i] = enemyStats.dropPrefabs[i];
            ds.dropChances[i] = enemyStats.dropChances[i];
        }

        hs = GetComponent<HealthSystem>();
        hs.maxHealth = enemyStats.life;
        hs.currentHealth = enemyStats.life;

        speed = enemyStats.speed;

        //Checking if its a shooter
        if (enemyStats.bulletType != null)
        {
            isShooter = true;

            shooter = GetComponentInChildren<Shooter>();
            shooter.bulletsList.Add(enemyStats.bulletType);

            shooter.StartBullet(shooter.bulletsList.Count - 1);
        }
    }

}
