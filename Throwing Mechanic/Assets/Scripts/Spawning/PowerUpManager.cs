using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : SpawnManager {

    public GameObject[] powerUpPrefabs;
    public ActivatePowerUps manager;

    private int numGoodPowerUps = 0;
    private int numRandomPowerUps = 0;

    public override void PlaySpawnSound()
    {
        instance.PlaySound("SpawnPowerUp");
    }

    public override void SetHasRequiredDeath()
    {
        hasRequiredDeath = false;
    }

    /// <summary>
    /// Spawns a powerup at a random spawn point
    /// </summary>
    public override void SpawnRandom()
    {
        // if spawns are available
        if (numObjects < numSpawns)
        {
            int rand = Random.Range(0, numSpawns);

            // if random spawn isn't already taken
            if (!activeSpawns[rand])
            {
                // randomly select powerup
                int prefabChoice = Random.Range(0, 2);

                // if good powerup selected
                if (prefabChoice == 1)
                {
                    // if good powerup is possible
                    if (manager.IsPowerUpPossible(PowerUpType.Good, numGoodPowerUps))
                    {
                        // spawn good powerup
                        DoSpawn(rand, prefabChoice);
                        numGoodPowerUps++;
                    }
                    // if not try random powerup
                    else if (manager.IsPowerUpPossible(PowerUpType.Random, numRandomPowerUps))
                    {
                        // spawn random powerup
                        DoSpawn(rand, 0);
                        numRandomPowerUps++;
                    }
                    else
                    {
                        Debug.Log("No powerups available");
                    }
                }
                else // random powerup selected
                {
                    // if random powerup is possible
                    if (manager.IsPowerUpPossible(PowerUpType.Random, numRandomPowerUps))
                    {
                        // spawn random powerup
                        DoSpawn(rand, prefabChoice);
                        numRandomPowerUps++;
                    }
                    // if not try good powerup
                    else if (manager.IsPowerUpPossible(PowerUpType.Good, numGoodPowerUps))
                    {
                        // spawn good powerup
                        DoSpawn(rand, 1);
                        numGoodPowerUps++;
                    }
                    else
                    {
                        Debug.Log("No powerups available");
                    }
                }
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
    /// Send spawn command and updates properties
    /// </summary>
    /// <param name="rand"></param>
    /// <param name="prefabChoice"></param>
    private void DoSpawn(int rand, int prefabChoice)
    {
        activeSpawns[rand] = true;
        numObjects++;
        spawnObjects[rand].Spawn(powerUpPrefabs[prefabChoice]);
        instance.PlaySound("SpawnPowerUp");
    }

    public void GoodPowerUpDestroyed(Transform parent)
    {
        numObjects--;
        numGoodPowerUps--;

        // Loop through spawners and find parent of destroyed target
        for (int i = 0; i < numSpawns; i++)
        {
            if (spawnObjects[i].transform == parent)
            {
                // Make spawn available for next target
                activeSpawns[i] = false;
            }
        }

        // increase streak for hitting powerup
        streak.IncreaseStreak();

    }

    public void RandomPowerUpDestroyed(Transform parent)
    {
        numObjects--;
        numRandomPowerUps--;

        // Loop through spawners and find parent of destroyed target
        for (int i = 0; i < numSpawns; i++)
        {
            if (spawnObjects[i].transform == parent)
            {
                // Make spawn available for next target
                activeSpawns[i] = false;
            }
        }

        // increase streak for hitting powerup
        streak.IncreaseStreak();
    }

    public int GetCurrentSpawned(PowerUpType type)
    {
        switch (type)
        {
            case PowerUpType.Good:
                return numGoodPowerUps;
            case PowerUpType.Random:
                return numRandomPowerUps;
        };

        return 0;
    }
}
