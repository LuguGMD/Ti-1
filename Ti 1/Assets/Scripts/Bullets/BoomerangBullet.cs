using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangBullet : Bullet
{
    //Time until going back
    float backTime = 1.5f;
    //Time until destroying
    float destroyTime;

    protected override void Start()
    {
        destroyTime = backTime * 1.5f;
        Invoke(nameof(GoBack), backTime);

        hs = GetComponent<HealthSystem>();
        StartStats();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            psPaint.startColor = color;
            Instantiate(psPaint, transform.position, transform.rotation);
        }
    }

    void GoBack()
    {
        //Making the Bullet Go Back
        speed *= -1;
        //Destroying self in seconds
        Destroy(gameObject, destroyTime);
    }


}
