using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickfirePowerUp : PowerUp {

    private ObjectManager objectManager;

    public QuickfirePowerUp()
    {
        label = "QUICKFIRE";
        isActive = false;
        resetTime = 30f;
        type = PowerUpType.Good;
    }

    private void Awake()
    {
        objectManager = FindObjectOfType<ObjectManager>();
    }

    protected override void Activate()
    {
        objectManager.SetPlayerFireRate(0.5f);
    }

    protected override void Deactivate()
    {
        objectManager.SetPlayerFireRate(2f);
    }
}
