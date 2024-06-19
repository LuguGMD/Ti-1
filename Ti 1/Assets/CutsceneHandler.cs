using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneHandler : MonoBehaviour
{

    public Image frame;
    public Sprite[] images;

    // Start is called before the first frame update
    void Start()
    {
        //Changin the cutscene image to the current level's cutscene
        frame.sprite = images[GameManager.main.currentLevel-1];
        frame.gameObject.SetActive(true);
    }

}
