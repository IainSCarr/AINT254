using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPowerUpManager : MonoBehaviour {

    public GameObject[] powerUpPrefabs;

    private Transform[] spawnArray;
    private bool[] activeSpawns;

    private int numSpawns;
    private int numPowerUps;

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

        numPowerUps = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.V))
        {
            SpawnRandomPowerUp(Random.Range(0, 2));
        }
    }

    private void SpawnRandomPowerUp(int prefabChoice)
    {
        // if spawns are available
        if (numPowerUps < numSpawns)
        {
            int rand = Random.Range(0, numSpawns);

            // if random spawn isn't already taken
            if (!activeSpawns[rand])
            {
                activeSpawns[rand] = true;
                numPowerUps++;
                spawnArray[rand].SendMessage("Spawn", powerUpPrefabs[prefabChoice]);
            }
            else // try again
            {
                SpawnRandomPowerUp(prefabChoice);
            }
        }
        else
        {
            Debug.Log("No spawn points available.");
        }
    }

    public void PowerUpDestroyed(Transform parent)
    {
        numPowerUps--;

        // Loop through spawners and find parent of destroyed powerup
        for (int i = 0; i < numSpawns; i++)
        {
            if (spawnArray[i] == parent)
            {
                // Make spawn available for next powerup
                activeSpawns[i] = false;
            }
        }
    }
}
