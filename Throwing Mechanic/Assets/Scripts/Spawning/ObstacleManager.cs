using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : SpawnManager {

    public override void PlaySpawnSound()
    {
    }

    public override void SetHasRequiredDeath()
    {
        hasRequiredDeath = false;
    }

    public override void SetIncreasesStreak()
    {
        increasesStreak = false;
    }
}
