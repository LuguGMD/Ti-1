using DG.Tweening;
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
    
    //Stores the time between checks of changing the state
    [SerializeField] float[] stateTime = new float[4];
    //Chances to move between states
    [SerializeField] float idleWanderChance;
    [SerializeField] float idleRushChance;
    [SerializeField] float wanderRushChance;
    [SerializeField] float wanderIdleChance;

    //Distances to end the states at
    float returnDist = 12f;
    float rushDist = -4f;

    [SerializeField] float speed;

    //Looking foward
    private Vector3 lookingDir = new Vector3(0f, 0f, 1f);
    private float lookingAngle = 0;
    private float minAngle = -90f;
    private float maxAngle = 90f;
    //Speed to move in wander state
    private float angleSpeed = 18f;
    //Direction to move in wander state
    private int dir = 1;

    public HealthSystem hs;

    enum State
    {
        Idle,
        Wander,
        Rush,
        Return
    }

    //Stores tge current state
    State currentState;

    void Start()
    {
        hs = GetComponent<HealthSystem>();
        //Setting start State
        currentState = State.Idle;
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
            else
            {
                //Running State Machine
                HandleState();
            }

        }
    }

    private void OnDestroy()
    {
        Actions.enemyKilled?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BulletPlayer"))
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
                tripleCopy.GetComponent<HealthSystem>().TakeDamage(1);
                dead = hs.TakeDamage(tripleCopy.damage);
            }
            else
            {
                //Making the bullet take damage
                bullet.GetComponent<HealthSystem>().TakeDamage(1);
                dead = hs.TakeDamage(bullet.damage);
            }

            if (dead)
            {
                Manager.main.AddPoint(5000);
                Destroy(gameObject);
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

            case State.Wander:
                RunWander();
            break;

            case State.Rush:
                RunRush();
            break;

            case State.Return:
                RunReturn();
            break;
        }
    }

    private void RunIdle()
    {
        //Checking if it's the first frame running
        if(isStart)
        {
            //Randomizing a chance
            float chance = Random.Range(0f, 100f);

            //Deciding the state to go to
            State state = currentState;

            //Checking which chance was beaten
            if(chance < idleRushChance)
            {
                state = State.Rush;
            }
            else if(chance < idleWanderChance + idleRushChance)
            {
                state = State.Wander;
            }

            //Going to it in a timer
            StartCoroutine(StartTransition(stateTime[(int)currentState], state));

            //Passing the first frame
            isStart = false;
        }
    }

    private void RunWander()
    {
        //Checking if it's the first frame running
        if (isStart)
        {
            //Randomizing a chance
            float chance = Random.Range(0f, 100f);

            //Changing direcions 25%
            if (chance <= 10f)
            {
                dir *= -1;
            }

            //Deciding the state to go to
            State state = currentState;

            //Checking which chance was beaten
            if (chance < wanderIdleChance)
            {
                state = State.Idle;
            }
            else if (chance < wanderRushChance + wanderIdleChance)
            {
                state = State.Rush;
            }

            //Going to it in a timer
            StartCoroutine(StartTransition(stateTime[(int)currentState], state));

            //Passing the first frame
            isStart = false;
        }

        //Rotating the looking angle
        lookingAngle += dir * angleSpeed * Time.deltaTime;

        //Clamping the value
        if (lookingAngle > maxAngle) dir *= -1;
        else if (lookingAngle < minAngle) dir *= -1;

        UpdatePos();

        //Looking at the center
        transform.LookAt(CameraController.main.transform);

    }

    private void RunRush()
    {
        //Checking if it's the first frame running
        if (isStart)
        {
            //Starting transition to other state
            StartCoroutine(StartTransition(stateTime[(int)currentState], State.Return));

            //Playing Sound
            AudioManager.main.PlaySFX((int)AudioManager.AudiosSFX.vacuum, true);

            //Passing the first frame
            isStart = false;
        }

        //Moving to target position
        DOTween.To(() => minDist, x => minDist = x, rushDist, stateTime[(int)currentState]);

        UpdatePos();

    }

    private void RunReturn()
    {
        //Checking if it's the first frame running
        if (isStart)
        {
            //Starting transition to other state
            StartCoroutine(StartTransition(stateTime[(int)currentState], State.Idle));

            //Passing the first frame
            isStart = false;
        }

        //Moving to target position
        DOTween.To(() => minDist, x => minDist = x, returnDist, stateTime[(int)currentState]);

        UpdatePos();

    }

    #endregion

    private void UpdatePos()
    {
        //Saving rotation
        Quaternion savedRotation = transform.rotation;

        //Teleporting to camera's position
        transform.position = CameraController.main.transform.position;
        //Reseting rotation
        transform.rotation = Quaternion.Euler(Vector3.zero);
        //Rotating towards destination
        transform.Rotate(Vector3.up * lookingAngle);
        //Saving the looking direction
        lookingDir = transform.forward;

        //Getting the target position
        Vector3 newPos = CameraController.main.transform.position + lookingDir * minDist;
        //Moving to the new location
        transform.position = newPos;

        //Restoring rotation
        transform.rotation = savedRotation;
    }

    private IEnumerator StartTransition(float time, State state)
    {
        //Waiting for the seconds passed
        yield return new WaitForSeconds(time);
        //Changing current state
        ChangeState(state);
    }
}
