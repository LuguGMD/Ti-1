using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public static CameraController main;

    public List<Waypoint> points = new List<Waypoint>();

    public float moveSpeed;
    public float rotateSpeed;

    private float currentAngle;
    private float targetAngle;
    private float dirAngle;

    private float reachDist = 0.2f;

    public float defaultDistance;
    public float cameraDistance;

    private bool canFollow = true;


    private void Awake()
    {
        if (main == null)
        {
            main = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Finding the path to follow
        GameObject path = GameObject.FindGameObjectWithTag("Path");
        //Getting the number of points
        int count = path.transform.childCount;
        for(int i = 0; i < count; i++)
        {
            //Adding the waypoints to the list
           points.Add(path.transform.GetChild(i).gameObject.GetComponent<Waypoint>());
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Manager.main.isGameRunning)
        {
            FollowPoint();
        }
    }

    private void LateUpdate()
    {
        Camera.main.transform.position = transform.position + Camera.main.transform.forward * -cameraDistance;
    }

    private void OnEnable()
    {
        //Subscribing to actions
        Actions.waveStarted += StopMoving;
        Actions.waveEnded += StartMoving;
    }

    private void OnDisable()
    {
        //Unsubscribing to actions
        Actions.waveStarted -= StopMoving;
        Actions.waveEnded -= StartMoving;
    }

    #region Movement
    void FollowPoint()
    {
        //Checking if there are any points left
        if (points.Count > 0 && canFollow == true)
        {

            //Getting the direction towards waypoint
            Vector3 dir = (points[0].transform.position - transform.position).normalized;
            //Getting the distance towards waypoint
            float dist = (points[0].transform.position - transform.position).magnitude;

            GetAngle();

            //Moving towards waypoint
            transform.position += dir * moveSpeed * Time.deltaTime;

            if (Mathf.Abs(currentAngle - targetAngle) > rotateSpeed * Time.deltaTime)
            {
                //Rotating towards waypoint
                currentAngle += rotateSpeed * dirAngle * Time.deltaTime;
            }

            //Rotating towards waypoint
            transform.rotation = new Quaternion(0f, currentAngle, 0f, transform.rotation.w);

            if (dist <= reachDist)
            {
                ReachPoint();
            }

        }
    }

    void ReachPoint()
    {
        //Checking if reached point is a trigger
        if (points[0].isTrigger)
        {
            //Invoking the unity Event from the reached point
            points[0].trigger?.Invoke();
        }

        //Removing reached point
        points.RemoveAt(0);
    }

    void GetAngle()
    {
        //Checking if there are any points left
        if (points.Count > 0)
        {
            //Getting the target angle
            transform.LookAt(points[0].transform.position);

            //Saving the target angle
            targetAngle = transform.rotation.y;

            dirAngle = Mathf.Sign(targetAngle - currentAngle);
        }
    }
    #endregion

    #region Control
    
    void StartMoving()
    {
        canFollow = true;
    }

    void StopMoving()
    {
        canFollow = false;
    }

    #endregion

    public void ChangeSpeed(float speed)
    {
        moveSpeed = speed;
    }

}
