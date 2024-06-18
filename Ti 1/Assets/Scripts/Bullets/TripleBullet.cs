using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleBullet : Bullet
{
    [SerializeField] GameObject bulletCopy;
    private float duration = 0.25f;

    // Start is called before the first frame update
    protected override void Start()
    {
        hs = GetComponent<HealthSystem>();
        StartStats();

        Destroy(gameObject, duration);

        //Getting parent rotation
        Vector3 rotation = transform.localEulerAngles;
        //Creating second Bullet
        rotation.y += 45;
        Quaternion rotationFirst = Quaternion.Euler(rotation);
        CreateBullet(rotationFirst);
        //Creatnig third Bullet
        rotation.y -= 90;
        Quaternion rotationSecond = Quaternion.Euler(rotation);
        CreateBullet(rotationSecond);
    }

    private void CreateBullet(Quaternion rotation)
    {
        TripleCopy bullet = Instantiate(bulletCopy, transform.position, rotation).GetComponent<TripleCopy>();

        Destroy(bullet.gameObject, duration);

        //Changing stats
        bullet.speed = bulletStats.speed;
        bullet.damage = bulletStats.damage;
        bullet.color = bulletStats.color;

        //Changing material
        var materialsCopy = GetComponent<MeshRenderer>().materials;
        materialsCopy[0] = bulletStats.material;
        bullet.GetComponent<MeshRenderer>().materials = materialsCopy;

        //Changing tag
        bullet.tag = bulletStats.tag;
    }

}
