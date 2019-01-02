using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : SpawnManager {

    private bool fastEnemies;

    public override void PlaySpawnSound()
    {
        instance.PlaySound("EnemySpawn");
    }

    public override void SetHasRequiredDeath()
    {
        hasRequiredDeath = true;
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
                spawnObjects[i].transform.GetChild(0).SendMessage("StartDeath", SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    /// <summary>
    /// Returns the number of enemies in the scene.
    /// </summary>
    public int GetCurrentEnemies()
    {
        return numObjects;
    }

    public override void SetObjectProperties(GameObject gameObject)
    {
        if (fastEnemies)
        {
            gameObject.GetComponent<EnemyMovement>().SetFastEnemy();
        }
    }

    public void SetFastEnemies(bool setting)
    {
        fastEnemies = setting;
    }
}
