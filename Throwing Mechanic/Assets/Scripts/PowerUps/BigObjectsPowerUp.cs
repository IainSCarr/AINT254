using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigObjectsPowerUp : PowerUp {

    private ObjectManager objectManager;

    public BigObjectsPowerUp()
    {
        label = "BIG OBJECTS";
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
        objectManager.SetBigObjects(true);
    }

    protected override void Deactivate()
    {
        objectManager.SetBigObjects(false);
    }
}
