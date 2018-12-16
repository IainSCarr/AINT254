using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : SpawnManager {

    private bool fastEnemies;

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
                //spawnArray[i].GetChild(0).SendMessage("StartDeath", SendMessageOptions.DontRequireReceiver);
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
            Debug.Log("Creating fast enemy");
            gameObject.GetComponent<EnemyMovement>().SetFastEnemy();
        }
        else
        {
            Debug.Log("Creating slow enemy");
        }
    }

    public void SetFastEnemies(bool setting)
    {
        fastEnemies = setting;
    }
}
