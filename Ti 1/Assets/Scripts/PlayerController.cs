using DG.Tweening;
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
        shooter = GetComponent<Shooter>();
        //Getting the health system component
        healthSystem = GetComponent<HealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Manager.main.isGameRunning)
        {
            Movement();
            Shooting();
        }
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
            other.GetComponent<HealthSystem>().TakeDamage(1);
        }
        else if(other.CompareTag("Heal"))
        {
            //Healing Self
            healthSystem.Heal(1);
            //Destroying heal object
            Destroy(other.gameObject);


            float perc = healthSystem.currentHealth / healthSystem.maxHealth;

            //Updating UIManager's life text
            UIManager.main.UpdateLifeText(perc);

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
        if (Q - E != 0)
        {
            //CHANGE COLOR FROM CONTAINER
            //ANIMATING
            transform.DOScale(0.8f, 0.1f).OnComplete(ReturnScaleEase).From(1f);
            shooter.bulletIndex += Q - E;

            //Changing the color of the bar to the color of the current bullet
            UIManager.main.lifeBar.color = shooter.bulletsList[shooter.bulletIndex].color;
            
            //Running through all the colors
            for (int i = 0; i < shooter.bulletsList.Count; i++)
            {
                //Gettting the index based on the selected bullet
                int j = i + shooter.bulletIndex;

                //Wrapping the array
                if(j >= shooter.bulletsList.Count) j -= shooter.bulletsList.Count;

                //Changing the color of the pen UI
                UIManager.main.colors[i].color = shooter.bulletsList[j].color;
            }
        }

    }

    void ReturnScaleEase()
    {
        transform.DOScale(1f, 1f).SetEase(Ease.OutElastic).From(0.8f);
    }
    void ReturnScale()
    {
        transform.DOScale(1f, 0.1f);
    }

    #endregion

    void TakeDamage(int damage)
    {
        //Taking the damage
        bool dead = healthSystem.TakeDamage(damage);

        float perc = healthSystem.currentHealth / healthSystem.maxHealth;

        //Updating UIManager's life text
        UIManager.main.UpdateLifeText(perc);

        //Playing hit sound
        AudioManager.main.PlaySFX((int)AudioManager.AudiosSFX.hit);

        //Checking if player is dead
        if(dead)
        {
            //Calling the action of player dead
            Actions.playerDead?.Invoke();
        }
    }

}
