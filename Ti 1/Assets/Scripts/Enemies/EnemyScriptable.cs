using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/new Enemy")]
public class EnemyScriptable : ScriptableObject
{
    public int life;
    public float speed;
    public BulletScriptable bulletType;

    public GameObject[] dropPrefabs;
    public float[] dropChances;
}
