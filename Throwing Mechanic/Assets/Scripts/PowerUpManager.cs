using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : SpawnManager {

    public GameObject[] powerUpPrefabs;

    public override void PlaySpawnSound()
    {
        instance.PlaySound("SpawnPowerUp");
    }

    public override void SetHasRequiredDeath()
    {
        hasRequiredDeath = false;
    }

    public override void SpawnRandom()
    {
        // if spawns are available
        if (numObjects < numSpawns)
        {
            int rand = Random.Range(0, numSpawns);
            int prefabChoice = Random.Range(0, 2);

            // if random spawn isn't already taken
            if (!activeSpawns[rand])
            {
                activeSpawns[rand] = true;
                numObjects++;
                spawnArray[rand].SendMessage("Spawn", powerUpPrefabs[prefabChoice]);
                instance.PlaySound("SpawnPowerUp");
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
}
