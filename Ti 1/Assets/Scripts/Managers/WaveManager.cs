using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    //Stores the waves for the current map
    public List<Wave> waves = new List<Wave>();
    private int currentWave;
    private int currentRound;

    public bool waveCleared = false;

    /*
     * 
     * Store position to spawn
     * 0 1 2 3 4
     * Left 
     * Middle Left
     * Middle
     * Middle Right
     * Right
     * 
     * */

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartWave()
    {
        //Invoking the wave started action
        Actions.waveStarted?.Invoke();
        StartCoroutine(RunWave());
    }

    IEnumerator RunWave()
    {
        //Running while has rounds to run
        while(currentRound < waves[currentWave].rounds.Count)
        {
            yield return new WaitForSeconds(1);
        }
    }

    public class Wave
    {
        //Stores the rounds of this wave
        public List<Round> rounds = new List<Round>();
    }

    public class Round
    {
        public List<Enemies> enemies = new List<Enemies>();
    }

    public struct Enemies
    {
        
    }
}
