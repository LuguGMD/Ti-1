using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{

    Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSFX()
    {
        //Getting the slider's value
        float value = slider.value;

        //Changing volume audio
        AudioManager.main.audioSFX.volume = value;
    }
    public void ChangeMSC()
    {
        //Getting the slider's value
        float value = slider.value;

        //Changing volume audio
        AudioManager.main.audioBG.volume = value;
    }

}
