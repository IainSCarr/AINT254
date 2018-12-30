using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTargetsPowerUp : PowerUp {

    private TargetManager targetManager;

    public MoveTargetsPowerUp()
    {
        label = "MOVING TARGETS";
        isActive = false;
        resetTime = 60f;
        type = PowerUpType.Bad;
    }

    private void Awake()
    {
        targetManager = FindObjectOfType<TargetManager>();
    }

    protected override void Activate()
    {
        targetManager.SetRotatingTargets(true);
    }

    protected override void Deactivate()
    {
        targetManager.SetRotatingTargets(false);
    }
}
