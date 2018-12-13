using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyPowerUp : PowerUp {

    private ObjectManager objectManager;

    public BouncyPowerUp()
    {
        label = "BOUNCY OBJECTS";
        isActive = false;
        resetTime = 30f;
        type = PowerUpType.Random;
    }

    private void Awake()
    {
        objectManager = FindObjectOfType<ObjectManager>();
    }

    protected override void Activate()
    {
        objectManager.SetBouncyObjects(true);
    }

    protected override void Deactivate()
    {
        objectManager.SetBouncyObjects(false);
    }
}
