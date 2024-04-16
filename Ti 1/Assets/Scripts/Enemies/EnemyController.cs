using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public EnemyScriptable enemyStats;
    public Shooter shooter;

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

    void StartStats()
    {
        HealthSystem hs = GetComponent<HealthSystem>();
        hs.maxHealth = enemyStats.life;
        hs.currentHealth = enemyStats.life;

        GetComponent<MeshFilter>().mesh = enemyStats.mesh;

        shooter = GetComponent<Shooter>();
        shooter.bulletsList.Add(enemyStats.bulletType);

        shooter.StartBullet(shooter.bulletsList.Count - 1);
    }

}
