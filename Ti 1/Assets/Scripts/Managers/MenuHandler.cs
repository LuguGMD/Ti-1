using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{

    public GameObject[] levels;


    // Start is called before the first frame update
    void Start()
    {
        //Checking if there are levels to be managed
        if (levels[0] != null)
        {
            //Running thorugh all the levels
            for (int i = 0; i < levels.Length; i++)
            {
                //Looking if the level is unlocked
                if (i < GameManager.main.currentLevel)
                {
                    //Changing the level's color
                    levels[i].GetComponent<Image>().color = Color.white;
                    levels[i].GetComponent<Button>().enabled = true;
                }
                else
                {
                    //Changing the level's color
                    levels[i].GetComponent<Image>().color = Color.black;
                    levels[i].GetComponent<Button>().enabled = false;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(int level)
    {
        //Loading the next Scene
        SceneManager.LoadScene(level);
    }

    public void NextLevel()
    {
        //Loading the next Level
        SceneManager.LoadScene(GameManager.main.currentLevel);
    }

    public void FirstStart()
    {
        //Checking if it's the first level
        if (GameManager.main.currentLevel == 1)
        {
            //Loading first Cutscene
            SceneManager.LoadScene(6);
        }
        else
        {
            //Loading Level Selector
            SceneManager.LoadScene(5);
        }
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }

}
