using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "Bullet/new Projectile")]
public class BulletScriptable : ScriptableObject
{
    [Header("Stats")]
    public float speed;
    public int damage;
    public GameObject bulletPrefab;
    public Mesh mesh;

    [Header("Shooting")]
    public int maxAmmo;
    public int currentAmmo;
    public float rechargeCooldown;
    public float shootCooldown;
    public bool canShoot;
    public bool isCharge;

    public void Init(BulletScriptable other)
    {
        this.speed = other.speed;
        this.damage = other.damage;
        this.bulletPrefab = other.bulletPrefab;
        this.mesh = other.mesh;

        this.maxAmmo = other.maxAmmo;
        this.currentAmmo = other.currentAmmo;
        this.rechargeCooldown = other.rechargeCooldown;
        this.shootCooldown = other.shootCooldown;
        this.canShoot = other.canShoot;
        this.isCharge = other.isCharge;
    }

}
