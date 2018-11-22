using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodePowerUp : PowerUp {

    private ObjectManager objectManager;

    public ExplodePowerUp()
    {
        label = "EXPLODING OBJECTS";
        isActive = false;
        resetTime = 30f;
        objectManager = FindObjectOfType<ObjectManager>();
    }

    public override void Activate()
    {
        objectManager.SetExplodableObjects(true);
        isActive = true;
    }

    public override void Deactivate()
    {
        objectManager.SetExplodableObjects(false);
        isActive = false;
    }
}
