using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatsManager : MonoBehaviour
{

    public float points = 0f;
    public PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Checking if player is dead
        if (player.life <= 0)
        {
            SceneManager.LoadScene("LoseScreen");
        }
    }
}
