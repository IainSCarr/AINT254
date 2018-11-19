using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour {

    public GameObject targetPrefab;

    private Transform[] spawnArray;
    private bool[] activeSpawns;

    private int numSpawns;
    private int numTargets;

    // Use this for initialization
    void Start()
    {
        numSpawns = transform.childCount;

        spawnArray = new Transform[numSpawns];
        activeSpawns = new bool[numSpawns];

        // Loop through all children and create an array of them
        for (int i = 0; i < numSpawns; i++)
        {
            spawnArray[i] = transform.GetChild(i);
        }

        numTargets = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            SpawnRandomTarget();
        }
    }

    private void SpawnRandomTarget()
    {
        // if spawns are available
        if (numTargets < numSpawns)
        {
            int rand = Random.Range(0, numSpawns);

            // if random spawn isn't already taken
            if (!activeSpawns[rand])
            {
                activeSpawns[rand] = true;
                numTargets++;
                spawnArray[rand].SendMessage("Spawn", targetPrefab);
            }
            else // try again
            {
                SpawnRandomTarget();
            }
        }
        else
        {
            Debug.Log("No spawn points available.");
        }
    }

    /// <summary>
    /// Updates state after target is destroyed.
    /// </summary>
    /// <param name="parent">The spawn point where the target was destroyed.</param>
    private void TargetDestroyed(Transform parent)
    {
        numTargets--;

        // Loop through spawners and find parent of destroyed target
        for (int i = 0; i < numSpawns; i++)
        {
            if (spawnArray[i] == parent)
            {
                // Make spawn available for next target
                activeSpawns[i] = false;
            }
        }
    }
}
