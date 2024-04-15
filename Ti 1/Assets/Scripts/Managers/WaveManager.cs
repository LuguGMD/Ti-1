using System;
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
    public bool roundCleared = false;

    //Distance from the camera in the Z axis to spawn the enemies
    private float zOffset = 60f;

    public enum SpawnPositions
    {
        Left, 
        MiddleLeft, 
        Middle, 
        MiddleRight, 
        Right, 
        RandomPos
    }

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

    public void SpawnEnemy(GameObject enemyPrefab, Vector3 enemySpawn)
    { 
        //Instantiating the enemy
        GameObject enemyInstance = Instantiate(enemyPrefab, enemySpawn, CameraController.main.transform.rotation);
        enemyInstance.transform.Rotate(Vector3.up * 180);
    }

    private Vector3 GetPos(SpawnPositions spawnEnum)
    {
        //Getting the boundary in the x axis
        float dir = PlayerController.main.xBoundary;
        
        switch(spawnEnum)
        {
            case SpawnPositions.Left:
                dir *= -1;
                break;
            case SpawnPositions.MiddleLeft:
                dir /= -2;
                break;
            case SpawnPositions.Middle:
                dir = 0;
                break;
            case SpawnPositions.MiddleRight:
                dir /= 2;
                break;
            case SpawnPositions.Right:

                break;
            case SpawnPositions.RandomPos:
                dir = UnityEngine.Random.Range(-dir, dir);
                break;
            default:
                dir = 0;
                break;

        }

        Vector3 pos = CameraController.main.transform.forward * zOffset +
                      CameraController.main.transform.right * dir +
                      CameraController.main.transform.position;

        return pos;
    }

    IEnumerator RunWave()
    {
        //Checkinf if there are still waves to run
        if (currentWave < waves.Count)
        {

            Wave wave = waves[currentWave];

            //Running while has rounds to run
            while (currentRound < wave.rounds.Count)
            {

                Round round = wave.rounds[currentRound];

                //Running through all enemies to spawn
                for (int i = 0; i < round.enemies.Count; i++)
                {
                    //Getting the enenmy spawn position
                    Vector3 spawnPos = GetPos(round.enemies[i].spawnPosition); ;

                    SpawnEnemy(round.enemies[i].instance, spawnPos);
                }

                //Waiting for round to be cleared
                yield return new WaitUntil(() => roundCleared == true);
                currentRound++;
                roundCleared = false;
            }

            //Calling the action wave ended
            Actions.waveEnded?.Invoke();
            //Resseting the rounds
            currentRound = 0;
            //Passing the wave
            currentWave++;
        }
    }

    #region WaveDefinition
    [Serializable]
    public class Wave
    {
        //Stores the rounds of this wave
        public List<Round> rounds = new List<Round>();
    }
    [Serializable]
    public class Round
    {
        public List<Enemies> enemies = new List<Enemies>();
    }
    [Serializable]
    public struct Enemies
    {
        public GameObject instance;
        public SpawnPositions spawnPosition;
    }
    #endregion
}
