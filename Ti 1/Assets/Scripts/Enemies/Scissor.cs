using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Scissor : EnemyController
{

    public override void Start()
    {
        base.Start();

        Destroy(gameObject, 5f);

        InvokeRepeating(nameof(playSound), 1f, 0.4f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Manager.main.isGameRunning)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    private void playSound()
    {
        //Playing Sound
        AudioManager.main.PlaySFX((int)AudioManager.AudiosSFX.cut, true);
    }
}
