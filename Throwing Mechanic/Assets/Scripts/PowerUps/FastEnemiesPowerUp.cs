using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemiesPowerUp : PowerUp {

    private EnemyManager enemyManager;

    public FastEnemiesPowerUp()
    {
        label = "FAST ENEMIES";
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
        enemyManager.SetFastEnemies(true);
    }

    protected override void Deactivate()
    {
        enemyManager.SetFastEnemies(false);
    }
}
