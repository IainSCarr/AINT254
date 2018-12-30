using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePowerUps : MonoBehaviour {

    public GameObject holder;

    // good powerups
    private ScorePowerUp scoreMultiplier;
    private KillEnemiesPowerUp killEnemies;
    private QuickfirePowerUp quickfire;

    // random effect powerups
    private ExplodePowerUp explode;
    private BouncyPowerUp bounce;
    private BigObjectsPowerUp bigObjects;

    // bad powerups
    public BarragePowerUp barrage;
    public FastEnemiesPowerUp fastEnemies;
    public MoveTargetsPowerUp moveTargets;

    private PowerUp[] goodPowerUps;
    private PowerUp[] badPowerUps;
    private PowerUp[] randomPowerUps;




    private int numActiveGoodPowerUps;
    private int numActiveBadPowerUps;
    private int numActiveRandomPowerUps;

    private bool powerupWasActivated;

    // Use this for initialization
    void Start () {
        // good powerups
        scoreMultiplier = holder.AddComponent<ScorePowerUp>();
        killEnemies = holder.AddComponent<KillEnemiesPowerUp>();
        quickfire = holder.AddComponent<QuickfirePowerUp>();

        //bad powerups
        barrage = holder.AddComponent<BarragePowerUp>();
        fastEnemies = holder.AddComponent<FastEnemiesPowerUp>();
        moveTargets = holder.AddComponent<MoveTargetsPowerUp>();

        // random powerups
        explode = holder.AddComponent<ExplodePowerUp>();
        bounce = holder.AddComponent<BouncyPowerUp>();
        bigObjects = holder.AddComponent<BigObjectsPowerUp>();

        goodPowerUps = new PowerUp[3] { scoreMultiplier, killEnemies, quickfire };

        badPowerUps = new PowerUp[3] { barrage, fastEnemies, moveTargets };

        randomPowerUps = new PowerUp[3] { explode, bounce, bigObjects };
    }

    public void DoGoodPowerUp()
    {
        // if not all powerups are active
        if (numActiveGoodPowerUps < goodPowerUps.Length)
        {
            // randomly choose a powerup
            int num = Random.Range(0, goodPowerUps.Length);

            // if it isn't active
            if (!goodPowerUps[num].GetIsActive())
            {
                // if it is possible to use that powerup
                if (goodPowerUps[num].GetIsPossible())
                {
                    goodPowerUps[num].Enable();

                    numActiveGoodPowerUps++;
                }
                else
                {
                    //if (IsPowerUpPossible(PowerUpType.Good)) // although powerup should only spawn if at least one powerup is possible this is a double check to prevent stack overflows
                    //{
                    //    DoGoodPowerUp();
                    //}
                    Debug.LogError("DEFENCES BREACHED! MASSIVE ERROR CALL THE POLICE");
                }
            }
            else
            {
                DoGoodPowerUp();
            }
        }
    }

    public void DoBadPowerUp()
    {
        if (numActiveBadPowerUps < badPowerUps.Length)
        {
            int num = Random.Range(0, badPowerUps.Length);

            if (!badPowerUps[num].GetIsActive())
            {
                badPowerUps[num].Enable();

                numActiveBadPowerUps++;
            }
            else
            {
                DoBadPowerUp();
            }
        }
    }

    public void DoRandomPowerUp()
    {
        if (numActiveRandomPowerUps < randomPowerUps.Length)
        {
            int num = Random.Range(0, randomPowerUps.Length);

            if (!randomPowerUps[num].GetIsActive())
            {
                randomPowerUps[num].Enable();

                numActiveRandomPowerUps++;
            }
            else
            {
                DoRandomPowerUp();
            }
        }
    }

    public void DoPowerUp(PowerUp powerUp)
    {

    }

    void OnEnable()
    {
        PowerUp.OnPowerUpDeactivated += HandleOnPowerUpDeactivated;
    }

    void OnDisable()
    {
        PowerUp.OnPowerUpDeactivated -= HandleOnPowerUpDeactivated;
    }

    public void HandleOnPowerUpDeactivated(string label, PowerUpType type)
    {
        if (type == PowerUpType.Good)
        {
            numActiveGoodPowerUps--;
        }
        else if (type == PowerUpType.Bad)
        {
            numActiveBadPowerUps--;
        }
        else
        {
            numActiveRandomPowerUps--;
        }
    }

    public bool IsPowerUpPossible(PowerUpType type, int currentSpawned)
    {
        if (type == PowerUpType.Good)
        {
            if (goodPowerUps.Length - numActiveGoodPowerUps - currentSpawned > 0)
            {
                for (int i = 0; i < goodPowerUps.Length; i++)
                {
                    if (!goodPowerUps[i].GetIsActive())
                    {
                        if (goodPowerUps[i].GetIsPossible())
                        {
                            Debug.Log("Good Powerup possible");
                            return true;
                        }
                    }
                }
            }

            Debug.Log("Good Powerup not possible");
            return false;
        }
        else if (type == PowerUpType.Bad)
        {
            for (int i = 0; i < badPowerUps.Length; i++)
            {
                if (!badPowerUps[i].GetIsActive())
                {
                    if (badPowerUps[i].GetIsPossible())
                    {
                        Debug.Log("Bad Powerup possible");
                        return true;
                    }
                }
            }

            Debug.Log("Bad Powerup not possible");
            return false;
        }
        else
        {
            if (randomPowerUps.Length - numActiveRandomPowerUps - currentSpawned > 0)
            {
                for (int i = 0; i < randomPowerUps.Length; i++)
                {
                    if (!randomPowerUps[i].GetIsActive())
                    {
                        if (randomPowerUps[i].GetIsPossible())
                        {
                            Debug.Log("Random Powerup possible");
                            return true;
                        }
                    }
                }
            }

            Debug.Log("Random Powerup not possible");
            return false;
        }
    }
}
