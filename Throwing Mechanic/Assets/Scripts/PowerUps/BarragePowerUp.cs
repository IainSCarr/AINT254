using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarragePowerUp : PowerUp {

    private EnemyManager enemyManager;

    public delegate void IncreaseFireRate(float rate);
    public static event IncreaseFireRate OnIncreaseFireRate;

    public BarragePowerUp()
    {
        label = "BARRAGE INCOMING";
        isActive = false;
        resetTime = 30f;
        type = PowerUpType.Bad;
    }

    private void Awake()
    {
        enemyManager = FindObjectOfType<EnemyManager>();
    }

    protected override void Activate()
    {
        if (enemyManager.GetCurrentEnemies() > 0)
        {
            if (OnIncreaseFireRate != null)
            {
                OnIncreaseFireRate(0.25f);
            }
        }
    }

    protected override void Deactivate()
    {
    }

    public override bool GetIsPossible()
    {
        if (enemyManager.GetCurrentEnemies() > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
