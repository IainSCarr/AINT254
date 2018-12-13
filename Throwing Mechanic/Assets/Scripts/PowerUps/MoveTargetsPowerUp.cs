using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTargetsPowerUp : PowerUp {

    //public delegate void MoveTargets(bool isActive);
    //public static event MoveTargets OnMoveTargets;

    public MoveTargetsPowerUp()
    {
        label = "MOVING TARGETS";
        isActive = false;
        resetTime = 30f;
        type = PowerUpType.Bad;
    }

    private void Awake()
    {
    }

    protected override void Activate()
    {
    }

    protected override void Deactivate()
    {
    }

    public override bool GetIsPossible()
    {
        return false;
    }
}
