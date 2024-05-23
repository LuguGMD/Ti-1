using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public static AudioManager main;

    public AudioSource audioBG;
    public AudioSource audioSFX;

    public AudioMixer audioMixer;

    [SerializeField] AudioClip[] bgList;
    [SerializeField] AudioClip[] sfxList;

    public enum AudiosBG
    {
        
    }

    public enum AudiosSFX
    {
        shoot,
        impact,
        hit
    }

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

    public void PlaySFX(int id, bool pitch = false)
    {
        if(pitch)
        {
            //Making pitch random
            audioMixer.SetFloat("pitch", Random.Range(0.8f, 1.2f));
        }
        else
        {
            //Making pitch normal
            audioMixer.SetFloat("pitch", 1f);
        }

        audioSFX.PlayOneShot(sfxList[id]);
    }

    public void ChangeMusic(int id)
    {
        audioBG.clip = bgList[id];
    }


}
