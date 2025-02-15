using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eraser : EnemyController
{

    private float sinCounter = 0f;

    [SerializeField] private float sinSpeed;
    [SerializeField] private float sinLength;

    [SerializeField] private AnimationCurve speedCurve;

    public override void Start()
    {
        base.Start();

        Destroy(gameObject, 7f);

        InvokeRepeating(nameof(playSound), 1f, 0.4f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Manager.main.isGameRunning)
        {
            //Adding the timer to the sin function
            sinCounter += Time.deltaTime;

            //Getting the value of the curve
            float curve = speedCurve.Evaluate(Mathf.Sin(sinCounter * sinSpeed));

            //Moving from side to side
            transform.position += transform.right * Mathf.Sin(sinCounter * sinSpeed) * sinLength * curve * Time.deltaTime;

            //Moving foward
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    private void playSound()
    {
        //Playing Sound
        AudioManager.main.PlaySFX((int)AudioManager.AudiosSFX.sweep, true);
    }
}
