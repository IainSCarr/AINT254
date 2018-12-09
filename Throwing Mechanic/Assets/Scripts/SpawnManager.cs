using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnManager : MonoBehaviour {

    public GameObject prefab;

    protected Transform[] spawnArray;
    protected bool[] activeSpawns;

    protected int numSpawns;
    protected int numObjects;

    protected bool hasRequiredDeath;

    protected AudioManager instance;

    // Use this for initialization
    void Start()
    {
        numSpawns = transform.childCount;

        spawnArray = new Transform[numSpawns];
        activeSpawns = new bool[numSpawns];

        SetHasRequiredDeath();

        // Loop through all children and create an array of them
        for (int i = 0; i < numSpawns; i++)
        {
            spawnArray[i] = transform.GetChild(i);
        }

        numObjects = 0;

        if (AudioManager.instance != null)
        {
            instance = AudioManager.instance;
        }
    }

    /// <summary>
    /// Spawn an object at a random spawn point.
    /// </summary>
    public void SpawnRandom()
    {
        // if spawns are available
        if (numObjects < numSpawns)
        {
            int rand = Random.Range(0, numSpawns);

            // if random spawn isn't already taken
            if (!activeSpawns[rand])
            {
                PlaySpawnSound();
                activeSpawns[rand] = true;
                numObjects++;
                spawnArray[rand].SendMessage("Spawn", prefab);
            }
            else // try again
            {
                SpawnRandom();
            }
        }
        else
        {
            Debug.Log("No spawn points available.");
        }
    }

    public abstract void PlaySpawnSound();
    public abstract void SetHasRequiredDeath();

    /// <summary>
    /// Updates state after object is destroyed.
    /// </summary>
    /// <param name="parent">The spawn point where the object was destroyed.</param>
    private void ObjectDestroyed(Transform parent)
    {
        numObjects--;

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

    public int GetNumberOfObjects()
    {
        return numObjects;
    }

    public bool GetHasRequiredDeath()
    {
        return hasRequiredDeath;
    }
}
