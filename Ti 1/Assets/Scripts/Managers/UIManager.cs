using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager main;

    public TextMeshProUGUI pointsText;
    public Image lifeBar;
    public Image winPanel;
    public Image losePanel;
    public Image pausePanel;

    public Image[] colors;

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

    private void OnEnable()
    {
        Actions.playerDead += LoseState;
        Actions.winRoom += WinState;
        Actions.pause += PauseState;
        Actions.resume += ResumeState;
    }

    private void OnDisable()
    {
        Actions.playerDead -= LoseState;
        Actions.winRoom -= WinState;
        Actions.pause -= PauseState;
        Actions.resume -= ResumeState;
    }

    public void UpdateLifeText(float perc)
    {
        lifeBar.rectTransform.localScale = new Vector3(perc, 1, 1);
    }

    public void WinState()
    {
        winPanel.gameObject.SetActive(true);
    }

    public void LoseState()
    {
        losePanel.gameObject.SetActive(true);
    }

    public void PauseState()
    {
        pausePanel.gameObject.SetActive(true);
    }

    public void ResumeState()
    {
        pausePanel.gameObject.SetActive(false);
    }
}
