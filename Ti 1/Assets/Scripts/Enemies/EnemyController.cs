using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public EnemyScriptable enemyStats;
    protected Shooter shooter;
    private HealthSystem hs;
    private DropSystem ds;

    private float points;

    protected float speed;

    private bool isShooter = false;

    public int damage = 1;

    // Start is called before the first frame update
    public virtual void Start()
    {
        StartStats();
    }

    // Update is called once per frame
    void Update()
    {
        if (Manager.main.isGameRunning)
        {
            if (isShooter)
            {
                shooter.Shoot();
            }
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("BulletPlayer"))
        {
            //Getting the projectile's bullet script
            Bullet bullet = other.GetComponent<Bullet>();
            //Stores if died
            bool dead;

            //Cheking if doesnt have bullet script
            if (bullet == null)
            {
                TripleCopy tripleCopy = other.GetComponent<TripleCopy>();
                //Making the bullet take damage
                tripleCopy.GetComponent<HealthSystem>().TakeDamage(damage);
                dead = hs.TakeDamage(tripleCopy.damage);
            }
            else
            {
                //Making the bullet take damage
                bullet.GetComponent<HealthSystem>().TakeDamage(damage);
                dead = hs.TakeDamage(bullet.damage);
            }

            if (dead)
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
        Manager.main.AddPoint(points);
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

        points = enemyStats.points;

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
