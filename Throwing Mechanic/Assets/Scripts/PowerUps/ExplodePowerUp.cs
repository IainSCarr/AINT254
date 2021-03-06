﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodePowerUp : PowerUp {

    private ObjectManager objectManager;

    public ExplodePowerUp()
    {
        label = "EXPLODING OBJECTS";
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
        objectManager.SetExplodableObjects(true);
    }

    protected override void Deactivate()
    {
        objectManager.SetExplodableObjects(false);
    }
}
