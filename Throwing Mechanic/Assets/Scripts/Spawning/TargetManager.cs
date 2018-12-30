using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : SpawnManager {

    private bool rotatingTargets;

    public override void PlaySpawnSound()
    {
        instance.PlaySound("TargetSpawn");
    }

    public override void SetHasRequiredDeath()
    {
        hasRequiredDeath = true;
    }

    public override void SetObjectProperties(GameObject gameObject)
    {
        if (rotatingTargets)
        {
            gameObject.GetComponent<PotBehaviour>().SetRotatingTargets();
        }
    }

    public void SetRotatingTargets(bool setting)
    {
        rotatingTargets = setting;
    }
}
