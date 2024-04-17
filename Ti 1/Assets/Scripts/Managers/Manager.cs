using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{

    public static Manager main;

    public bool isGameRunning = true;

    public float points = 0f;

    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    void Update()
    {
        //Getting player's Input
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            EscPressed();
        }
    }

    private void OnEnable()
    {
        Actions.playerDead += PauseGame;
        Actions.winRoom += PauseGame;
        Actions.pause += PauseGame;
        Actions.resume += PlayGame;
    }

    private void OnDisable()
    {
        Actions.playerDead -= PauseGame;
        Actions.winRoom -= PauseGame;
        Actions.pause -= PauseGame;
        Actions.resume -= PlayGame;
    }

    #region Pause

    public void PauseGame()
    {
        isGameRunning = false;
    }

    public void PlayGame()
    {
        isGameRunning = true;
    }

    public void EscPressed()
    {
        if (!isGameRunning)
        {
            Actions.resume?.Invoke();
        }
        else
        {
            Actions.pause?.Invoke();
        }
    }

    #endregion

    #region States

    public void WinRoom()
    {
        Actions.winRoom?.Invoke();
    }

    public void RestartRoom()
    {
        //Loading the currently loaded scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    #endregion

    public void AddPoint(float pointsToAdd = 1)
    {
        points += pointsToAdd;
        UIManager.main.pointsText.text = "Points: " + points.ToString();
    }

}
