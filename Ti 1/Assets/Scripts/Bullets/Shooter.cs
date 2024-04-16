using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    //Holds the available bullets to shoot
    public List<BulletScriptable> bulletsList = new List<BulletScriptable>();
    
    

    //The index of the slected bullet
    int m_bulletIndex = 0;

    public int bulletIndex
    {
        get { return m_bulletIndex; }
        set
        {
            if (value >= bulletsList.Count)
            {
                m_bulletIndex = 0;
            }
            else if(value < 0) 
            {
                m_bulletIndex = bulletsList.Count-1;
            }
            else
            {
                m_bulletIndex = value;
            }
        }
    }

    private void Start()
    {
        //Going through all bullets
       for(int i = 0; i < bulletsList.Count; i++)
        {
            StartBullet(i);
        }
    }

    public void Shoot()
    {
        BulletScriptable bullet = bulletsList[bulletIndex];
        //Checking if selected bullet can shoot
        if (bullet.canShoot)
        {
            //Instantiating bullet
            GameObject bulletInstance = Instantiate(bullet.bulletPrefab, transform.position, transform.rotation);
            //Passing the values of the bullet type to the bullet
            bulletInstance.GetComponent<Bullet>().bulletStats = bullet;

            //Reducing ammo from the bullet
            bullet.currentAmmo--;
            //Disabling shooting
            bullet.canShoot = false;
            StartCoroutine(Recharge());
        }
    }

    IEnumerator Recharge()
    {
        BulletScriptable bullet = bulletsList[bulletIndex];
        //Checking if there is ammo left
        if (bullet.currentAmmo <= 0)
        {
            //Wainting for ammo recharge cooldown
            yield return new WaitForSeconds(bullet.rechargeCooldown);
            //Recharging ammo
            bullet.currentAmmo = bullet.maxAmmo;
        }
        else
        {
            //Wainting for shooting cooldown
            yield return new WaitForSeconds(bullet.shootCooldown);
        }
        //Enabling shooting
        bullet.canShoot = true;
    }

    public void StartBullet(int ind)
    {
        BulletScriptable newBullet = ScriptableObject.CreateInstance<BulletScriptable>();

        newBullet.Init(bulletsList[ind]);

        bulletsList[ind] = newBullet;

        newBullet.currentAmmo = newBullet.maxAmmo;
        newBullet.canShoot = true;
    }

}
