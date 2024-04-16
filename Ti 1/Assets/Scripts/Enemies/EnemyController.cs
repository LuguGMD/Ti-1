using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public EnemyScriptable enemyStats;
    public Shooter shooter;
    private HealthSystem hs;
    private DropSystem ds;

    public int damage = 1;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        StartStats();
    }

    // Update is called once per frame
    void Update()
    {
        shooter.Shoot();
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

        GetComponent<MeshFilter>().mesh = enemyStats.mesh;

        shooter = GetComponent<Shooter>();
        shooter.bulletsList.Add(enemyStats.bulletType);

        shooter.StartBullet(shooter.bulletsList.Count - 1);
    }

}
