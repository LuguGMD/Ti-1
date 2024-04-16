using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSystem : MonoBehaviour
{

    public GameObject[] dropPrefabs;
    public float[] dropChances;

    public void Drop(Vector3 spawnPosition)
    {
        //Running through all droppable items
        for (int i = 0; i < dropPrefabs.Length; i++)
        {
            //Running the odds
            if(Random.Range(0, 100)  <= dropChances[i])
            {
                //Instantiating the dropped item at the spawn position
                Instantiate(dropPrefabs[i], spawnPosition, dropPrefabs[i].transform.rotation);
            }
        }
    }
}
