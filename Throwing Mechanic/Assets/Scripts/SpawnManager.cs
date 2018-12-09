using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnManager : MonoBehaviour {

    public GameObject[] prefabs;

    private Transform[] spawnArray;
    private bool[] activeSpawns;

    private int numSpawns;
    private int numObjects;

    private AudioManager instance;

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

        numObjects = 0;

        if (AudioManager.instance != null)
        {
            instance = AudioManager.instance;
        }
    }

    public abstract void SpawnRandom();

    /// <summary>
    /// Updates state after target is destroyed.
    /// </summary>
    /// <param name="parent">The spawn point where the target was destroyed.</param>
    private void TargetDestroyed(Transform parent)
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
}
