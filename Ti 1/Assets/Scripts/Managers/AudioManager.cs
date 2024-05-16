using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager main;

    private AudioSource audioSource;
    public AudioClip shootSFX;
    public AudioClip impactWoodSFX;

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
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        audioSource.clip = shootSFX;
        PlayRandom();
    }

    public void Wood()
    {
        audioSource.clip = impactWoodSFX;
        PlayRandom();
    }

    private void PlayRandom()
    {
        //Randomizing SFX Pitch
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.Play();
    }

    private void PlayNormal()
    {
        audioSource.Play();
    }

}
