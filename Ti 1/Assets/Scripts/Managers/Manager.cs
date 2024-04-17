using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        //Subscribing RestarRoom method to player dead action
        Actions.playerDead += RestartRoom;
    }

    private void OnDisable()
    {
        //Unsubscribing RestarRoom method to player dead action
        Actions.playerDead -= RestartRoom;
    }

    public void RestartRoom()
    {
        //Loading the currently loaded scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
