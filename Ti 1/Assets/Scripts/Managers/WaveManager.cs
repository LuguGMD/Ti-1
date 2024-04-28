using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    //Stores the waves for the current map
    public List<Wave> waves = new List<Wave>();
    private int currentWave;
    private int currentRound;

    public bool waveCleared = false;
    public bool roundCleared = false;

    private int enemyCount = 0;

    //Distance from the camera in the Z axis to spawn the enemies
    private float zOffset = 40f;

    public enum SpawnPositions
    {
        Left, 
        MiddleLeft, 
        Middle, 
        MiddleRight, 
        Right, 
        RandomPos
    }

    public enum SpawnDirections
    {
        Front,
        Left,
        Right,
        RandomDir
    }

    private void OnEnable()
    {
        Actions.enemyKilled += EnemyKilled;
    }

    private void OnDisable()
    {
        Actions.enemyKilled -= EnemyKilled;
    }

    private void EnemyKilled()
    {
        enemyCount--;

        //Checking if there aren't enemies left
        if (enemyCount <= 0)
        {
            roundCleared = true;
        }
    }

    public void StartWave()
    {
        //Invoking the wave started action
        Actions.waveStarted?.Invoke();

        roundCleared = false;
        waveCleared = false;

        currentRound = 0;

        StartCoroutine(RunWave());
    }

    public void SpawnEnemy(GameObject enemyPrefab, Vector3 enemySpawn, Quaternion spawnDir)
    { 
        //Instantiating the enemy
        GameObject enemyInstance = Instantiate(enemyPrefab, enemySpawn, spawnDir);
        enemyInstance.transform.Rotate(Vector3.up * 180);

        enemyCount++;
    }

    private Vector3 GetPos(SpawnPositions spawnEnum, SpawnDirections spawnDir, int ind)
    {
        //Getting the boundary in the x axis
        float dir = PlayerController.main.xBoundary;

        switch (spawnEnum)
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


        if (spawnDir == SpawnDirections.RandomDir)
        {
            spawnDir = (SpawnDirections)ind;
        }


        switch (spawnDir)
        {
            case SpawnDirections.Left:
                pos = CameraController.main.transform.forward * dir +
                      CameraController.main.transform.right * -zOffset  +
                      CameraController.main.transform.position;
                break;
            case SpawnDirections.Right:
                pos = CameraController.main.transform.forward * dir +
                      CameraController.main.transform.right * zOffset +
                      CameraController.main.transform.position;
                break;
            case SpawnDirections.Front:
                pos = CameraController.main.transform.forward * zOffset +
                      CameraController.main.transform.right * dir +
                      CameraController.main.transform.position;
                break;
        }


        return pos;
    }

    private Quaternion GetAng(SpawnDirections spawnDir, int ind)
    {
        Quaternion dir = CameraController.main.transform.rotation;

        if(spawnDir == SpawnDirections.RandomDir)
        {
            spawnDir = (SpawnDirections)ind;
        }

        switch(spawnDir)
        {
            case SpawnDirections.Left:
                dir.eulerAngles = new Vector3(dir.x, dir.y - 90, dir.z);
                break;
            case SpawnDirections.Right:
                dir.eulerAngles = new Vector3(dir.x, dir.y + 90, dir.z);
                break;
            case SpawnDirections.Front:
                dir.eulerAngles = new Vector3(dir.x, dir.y, dir.z);
                break;

        }

        return dir;
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

                    int ind = 0;
                    SpawnDirections enemyDir = round.enemies[i].spawnDirection;

                    //Looking if the enemy has a random position to spáwn
                    if (enemyDir == SpawnDirections.RandomDir)
                    {
                        string[] names = Enum.GetNames(enemyDir.GetType());
                        ind = UnityEngine.Random.Range(0, names.Length - 1);
                    }


                    //Getting the enenmy spawn position
                    Vector3 spawnPos = GetPos(round.enemies[i].spawnPosition, enemyDir, ind); ;
                    Quaternion spawnDir = GetAng(enemyDir, ind);

                    SpawnEnemy(round.enemies[i].instance, spawnPos, spawnDir);
                }

                //Waiting for round to be cleared
                yield return new WaitUntil(() => roundCleared == true);
                currentRound++;
                roundCleared = false;
            }

            waveCleared = true;
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
        public SpawnDirections spawnDirection;
    }
    #endregion
}
