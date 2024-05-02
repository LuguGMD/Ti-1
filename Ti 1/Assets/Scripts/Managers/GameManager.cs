using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager main;

    public int currentLevel = 1;

    private void Awake()
    {
        if (main == null)
        {
            main = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        //Cheat codes for changing levels
        if(Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.LoadScene(1);
        }
        else if(Input.GetKeyDown(KeyCode.F2))
        {
            SceneManager.LoadScene(2);
        }
        else if(Input.GetKeyDown(KeyCode.F3))
        {
            SceneManager.LoadScene(3);
        }else if(Input.GetKeyDown(KeyCode.F12))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnEnable()
    {
        Actions.winRoom += PassLevel;
    }

    private void OnDisable()
    {
        Actions.winRoom -= PassLevel;
    }

    void PassLevel()
    {
        //Getting the index of the current cleared level
        int levelPassed = SceneManager.GetActiveScene().buildIndex;
        //Checking if level wasn't beat before
        if (levelPassed >= currentLevel)
        {
            //Passing the current level
            currentLevel = levelPassed+1;
        }
    }
}