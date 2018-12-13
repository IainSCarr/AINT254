using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePowerUp : PowerUp {

    private ScoreManager scoreManager;

    public ScorePowerUp()
    {
        label = "X2 SCORE MULTIPLIER";
        isActive = false;
        resetTime = 30f;
        type = PowerUpType.Good;
    }

    private void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    protected override void Activate()
    {
        scoreManager.SetDoubledScore(true);
    }

    protected override void Deactivate()
    {
        scoreManager.SetDoubledScore(false);
    }
}
