using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnManager : MonoBehaviour {

    public GameObject prefab;

    protected SpawnObject[] spawnObjects;
    protected bool[] activeSpawns;

    protected int numSpawns;
    protected int numObjects;

    protected bool increasesStreak;
    protected bool hasRequiredDeath;

    protected AudioManager instance;
    protected StreakController streak;

    // Use this for initialization
    void Start()
    {
        numSpawns = transform.childCount;

        spawnObjects = new SpawnObject[numSpawns];
        activeSpawns = new bool[numSpawns];

        // Loop through all children and create an array of them
        for (int i = 0; i < numSpawns; i++)
        {
            spawnObjects[i] = transform.GetChild(i).GetComponent<SpawnObject>();
        }

        numObjects = 0;

        if (AudioManager.instance != null)
        {
            instance = AudioManager.instance;
        }

        SetHasRequiredDeath();
        SetIncreasesStreak();

        if (increasesStreak)
        {
            streak = FindObjectOfType<StreakController>();
        }
    }

    /// <summary>
    /// Spawn an object at a random spawn point.
    /// </summary>
    public virtual void SpawnRandom()
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
                GameObject tempObject = spawnObjects[rand].Spawn(prefab);
                SetObjectProperties(tempObject);
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
            if (spawnObjects[i].transform == parent)
            {
                // Make spawn available for next target
                activeSpawns[i] = false;
            }
        }

        if (increasesStreak)
        {
            streak.IncreaseStreak();
        }
    }

    public virtual void PlaySpawnSound()
    {

    }

    public abstract void SetHasRequiredDeath();

    public virtual void SetIncreasesStreak()
    {
        increasesStreak = true;
    }

    public virtual void SetObjectProperties(GameObject gameObject)
    {

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
