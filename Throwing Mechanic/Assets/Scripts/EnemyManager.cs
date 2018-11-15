using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public GameObject enemyPrefab;

    private Transform[] spawnArray;
    private bool[] activeSpawns;

    private int numSpawns;
    private int numEnemies;

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

        numEnemies = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SpawnRandomEnemy();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            DestroyAllEnemies();
        }
	}

    private void SpawnRandomEnemy()
    {
        // if spawns are available
        if (numEnemies < numSpawns)
        {
            int rand = Random.Range(0, numSpawns);

            // if random spawn isn't already taken
            if (!activeSpawns[rand])
            {
                activeSpawns[rand] = true;
                numEnemies++;
                spawnArray[rand].SendMessage("Spawn", enemyPrefab);
            }
            else // try again
            {
                SpawnRandomEnemy();
            }
        }
        else
        {
            Debug.Log("No spawn points available.");
        }
    }

    /// <summary>
    /// Updates state after enemy is destroyed.
    /// </summary>
    /// <param name="parent">The spawn point where the enemy was destroyed.</param>
    private void EnemyDestroyed(Transform parent)
    {
        numEnemies--;

        // Loop through spawners and find parent of destroyed enemy
        for (int i = 0; i < numSpawns; i++)
        {
            if (spawnArray[i] == parent)
            {
                // Make spawn available for next enemy
                activeSpawns[i] = false;
            }
        }
    }

    public void DestroyAllEnemies()
    {
        // Loop through all spawns
        for (int i = 0; i < numSpawns; i++)
        {
            // If spawn has enemy
            if (activeSpawns[i])
            {
                // Send self destruct message
                spawnArray[i].GetChild(0).SendMessage("StartDeath", SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    /// <summary>
    /// Returns the number of enemies in the scene.
    /// </summary>
    /// <returns>An integer of the number of enemies.</returns>
    public int GetCurrentEnemies()
    {
        return numEnemies;
    }
}
