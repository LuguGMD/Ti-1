using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pencil : EnemyController
{
    //Anchor variables
    [SerializeField] private GameObject anchorPoint;
    [SerializeField] private float anchorHeight;

    [Header("Attack")]
    [SerializeField] private AnimationCurve attackCurve;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackCurveValue;
    private float attackTime;
    [SerializeField] private float attackDuration;
    [SerializeField] private float attackDelay;

    [SerializeField] private float yOffset = 3f;

    private float distAnchor;

    private GameObject player;
    private PencilStates currentState;

    [Header("Chase")]
    //Chasing variables
    private float playerDist;
    [SerializeField] private float minDist;

    enum PencilStates
    {
        Chase,
        Recharge,
        Attack,
        Wait
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        //Getting the start state
        currentState = PencilStates.Chase;

        //Getting the position of the anchor point and moving upward
        transform.position = anchorPoint.transform.position + anchorPoint.transform.up * anchorHeight;

        //Getting a reference to the player
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Manager.main.isGameRunning)
        {

            switch(currentState)
            {
                case PencilStates.Chase:
                    Chase();
                    break;

                case PencilStates.Recharge:
                    Recharge();
                    break;

                case PencilStates.Attack:
                    Attack();
                    break; 

                case PencilStates.Wait:

                    break;
                default:
                    currentState = PencilStates.Chase;    
                break;


            }
        }
    }

    void Chase()
    {
        Vector3 playerPos = new Vector3(player.transform.position.x, anchorPoint.transform.position.y, player.transform.position.z);

        //Getting the distance between enemy and player
        playerDist = (anchorPoint.transform.position - playerPos).magnitude;

        Vector3 dir = (playerPos - anchorPoint.transform.position).normalized;

        //Moving towards player
        anchorPoint.transform.position += dir * speed * Time.deltaTime;

        //Checking if enemy is near enough
        if (playerDist <= minDist)
        {
            StartCoroutine(ChangeState(PencilStates.Attack));
        }
    }
    void Recharge()
    {

        //Decrease the timer
        attackTime -= attackDelay * Time.deltaTime;

        //Clamping the attack time
        attackTime = Mathf.Clamp(attackTime, 0, attackDelay);

        //Evaluating the curve
        attackCurveValue = attackTime / attackDelay;

        //Getting the offset to move from to the anchor point
        float yDist = yOffset * attackCurveValue + distAnchor - distAnchor * attackCurveValue;

        //Moving the pencil
        transform.position = new Vector3(anchorPoint.transform.position.x, yDist, anchorPoint.transform.position.z);

        //Checking if enough time passed
        if (attackTime <= 0)
        {
            //Reseting the timer
            attackTime = 0;
            //Changing state
            StartCoroutine(ChangeState(PencilStates.Chase));
        }

    }
    void Attack()
    {
        //Just entered the state
        if(attackTime == 0)
        {
            //Getting the distance to the anchor point
            distAnchor = (transform.position - anchorPoint.transform.position).magnitude;
        }

        //Increasing the timer
        attackTime += attackSpeed * Time.deltaTime;

        //Clamping the attack time
        attackTime = Mathf.Clamp(attackTime, 0, attackDuration);

        //Evaluating the curve
        attackCurveValue = attackCurve.Evaluate(attackTime/attackDuration);

        //Getting the offset to move from to the anchor point
        float yDist = yOffset * attackCurveValue + distAnchor - distAnchor * attackCurveValue;

        //Moving the pencil
        transform.position = new Vector3(anchorPoint.transform.position.x, yDist,anchorPoint.transform.position.z);


        //Checking if the target is there
        if(attackTime/attackDuration == 1)
        {
            //Changing the timer to the delay
            attackTime = attackDelay;
            //Changing the state
            StartCoroutine(ChangeState(PencilStates.Recharge, attackDelay));
        }

    }

    IEnumerator ChangeState(PencilStates state, float wait = 0f)
    {
        currentState = PencilStates.Wait;
        yield return new WaitForSeconds(wait);
        currentState = state;
    }

}
