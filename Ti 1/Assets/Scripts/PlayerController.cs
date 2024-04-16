using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController main;

    [Header("Movement")]
    Transform pivot;

    float horizontalInput;
    float verticalInput;

    public float speed;
    public float floatSpeed;

    public float floatHeight;
    public float xBoundary;
    public float zBoundary;

    float xOffset;
    float zOffset;
    float yOffset;

    private Shooter shooter;
    private HealthSystem healthSystem;

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
        //Getting the camera controller position
        pivot = CameraController.main.transform;
        //Getting the shooter component
        shooter = GetComponentInChildren<Shooter>();
        //Getting the health system component
        healthSystem = GetComponent<HealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
    }

    private void LateUpdate()
    {
        LateMovement();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            TakeDamage(other.GetComponent<EnemyController>().damage);
        }
        else if(other.CompareTag("BulletEnemy"))
        {
            TakeDamage(other.GetComponent<Bullet>().damage);
            Destroy(other.gameObject);
        }
        else if(other.CompareTag("Heal"))
        {
            //Healing Self
            healthSystem.Heal(1);
            //Destroying heal object
            Destroy(other.gameObject);
        }
    }

    #region Control
    void Movement()
    {
        //Getting player's input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //Moving the player based on the pivot point
        xOffset += horizontalInput * speed * Time.deltaTime;
        zOffset += verticalInput * speed * Time.deltaTime;

        //Making the player float
        yOffset = (Mathf.Cos(Time.fixedTime * floatSpeed) * floatHeight) + floatHeight;
    }

    void LateMovement()
    {
        //Clamping player position
        xOffset = Mathf.Clamp(xOffset, -xBoundary, xBoundary);
        zOffset = Mathf.Clamp(zOffset, -zBoundary, zBoundary);

        //Getting the offset in all directions into one Vector3
        Vector3 offset = pivot.right * xOffset + pivot.forward * zOffset + pivot.up * yOffset;

        //Updating the player position
        transform.position = pivot.position + offset;
    }

    void Shooting()
    {
        //Getting the shooting input
        if (Input.GetKey(KeyCode.Space))
        {
            shooter.Shoot();
        }

        //Getting the inputs to change the bullet type
        int Q = Input.GetKeyDown(KeyCode.Q) ? 0 : 1;
        int E = Input.GetKeyDown(KeyCode.E) ? 0 : 1;

        //Changing the index of the shooter
        if (Q - E != 0) shooter.bulletIndex += Q - E;

    }
    #endregion

    void TakeDamage(int damage)
    {
        //Taking the damage
        bool dead = healthSystem.TakeDamage(damage);

        //Checking if player is dead
        if(dead)
        {
            //Calling the action of player dead
            Actions.playerDead?.Invoke();
        }
    }

}
