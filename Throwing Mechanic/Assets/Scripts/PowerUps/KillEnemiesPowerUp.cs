﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemiesPowerUp : PowerUp {

    private EnemyManager enemyManager;

    public KillEnemiesPowerUp()
    {
        label = "ALL ENEMIES KILLED";
        isActive = false;
        resetTime = 30f;
        type = PowerUpType.Good;
    }

    private void Awake()
    {
        enemyManager = FindObjectOfType<EnemyManager>();
    }

    protected override void Activate()
    {
        enemyManager.DestroyAllEnemies();

        Debug.Log(label + " activated.");
    }

    protected override void Deactivate()
    {
        Debug.Log(label + " deactivated.");
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
