using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private float dist;
    public float minDist;

    private bool isNear = false;
    //Stores if the frame is the first one of the state
    private bool isStart = true;

    [SerializeField] float speed;

    enum State
    {
        Idle,
    }

    //Stores tge current state
    State currentState;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Manager.main.isGameRunning)
        {
            if (!isNear)
            {
                //Getting the distance between enemy and player
                dist = (transform.position - CameraController.main.transform.position).magnitude;

                //Moving towards player
                transform.position += transform.forward * speed * Time.deltaTime;

                //Checking if enemy is near enough
                if (dist <= minDist)
                {
                    isNear = true;
                }
            }
        }
    }

    private void ChangeState(State state)
    {
        //Changing the current state
        currentState = state;
        //Reseting start value
        isStart = true;
    }

    #region STATE MACHINE

    private void HandleState()
    {
        switch (currentState)
        {
            case State.Idle:
                RunIdle();
            break;
        }
    }

    private void RunIdle()
    {

    }

    #endregion
}
