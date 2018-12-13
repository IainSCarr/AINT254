using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : SpawnManager {

    public override void PlaySpawnSound()
    {
        
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
                spawnArray[i].GetChild(0).SendMessage("StartDeath", SendMessageOptions.DontRequireReceiver);
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
}
