using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : SpawnManager {

    public override void PlaySpawnSound()
    {
        instance.PlaySound("TargetSpawn");
    }

    public override void SetHasRequiredDeath()
    {
        hasRequiredDeath = true;
    }
}
