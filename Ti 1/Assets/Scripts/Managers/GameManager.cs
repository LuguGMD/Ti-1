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
        int levelPassed = SceneManager.GetActiveScene().buildIndex;
        if (levelPassed >= currentLevel)
        {
            currentLevel = levelPassed+1;
        }
    }
}
