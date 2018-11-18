using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour {

    public GameObject obstaclePrefab;

    private Transform[] spawnArray;
    private bool[] activeSpawns;

    private int numSpawns;
    private int numObstacles;

    // Use this for initialization
    void Start () {
        numSpawns = transform.childCount;

        spawnArray = new Transform[numSpawns];
        activeSpawns = new bool[numSpawns];

        // Loop through all children and create an array of them
        for (int i = 0; i < numSpawns; i++)
        {
            spawnArray[i] = transform.GetChild(i);
        }

        numObstacles = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.B))
        {
            SpawnRandomObstacle();
        }
	}

    private void SpawnRandomObstacle()
    {
        // if spawns are available
        if (numObstacles < numSpawns)
        {
            int rand = Random.Range(0, numSpawns);

            // if random spawn isn't already taken
            if (!activeSpawns[rand])
            {
                activeSpawns[rand] = true;
                numObstacles++;
                spawnArray[rand].SendMessage("Spawn", obstaclePrefab);
            }
            else // try again
            {
                SpawnRandomObstacle();
            }
        }
        else
        {
            Debug.Log("No spawn points available.");
        }
    }

    /// <summary>
    /// Updates state after obstacle is destroyed.
    /// </summary>
    /// <param name="parent">The spawn point where the obstacle was destroyed.</param>
    private void ObstacleDestroyed(Transform parent)
    {
        numObstacles--;

        // Loop through spawners and find parent of destroyed obstacle
        for (int i = 0; i < numSpawns; i++)
        {
            if (spawnArray[i] == parent)
            {
                // Make spawn available for next obstacle
                activeSpawns[i] = false;
            }
        }
    }
}
