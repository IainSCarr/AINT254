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

    // arrays of all powerups of a type
    private PowerUp[] goodPowerUps;
    private PowerUp[] badPowerUps;
    private PowerUp[] randomPowerUps;

    // number of active powerups of a type
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

    /// <summary>
    /// Activates a random good powerup.
    /// </summary>
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

    // <summary>
    // Activates a random bad powerup
    // </summary>
    //public void DoBadPowerUp()
    //{
    //    if (numActiveBadPowerUps < badPowerUps.Length)
    //    {
    //        int num = Random.Range(0, badPowerUps.Length);

    //        if (!badPowerUps[num].GetIsActive())
    //        {
    //            badPowerUps[num].Enable();

    //            numActiveBadPowerUps++;
    //        }
    //        else
    //        {
    //            DoBadPowerUp();
    //        }
    //    }
    //}

    /// <summary>
    /// Activates a random random powerup
    /// </summary>
    public void DoRandomPowerUp()
    {
        // if not all powerups are active
        if (numActiveRandomPowerUps < randomPowerUps.Length)
        {
            // randomly choose a powerup
            int num = Random.Range(0, randomPowerUps.Length);

            // if it isn't active
            if (!randomPowerUps[num].GetIsActive())
            {
                // random powerups are always possible so no check needed
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

    /// <summary>
    /// Checks if a certain type of powerup is possible.
    /// </summary>
    /// <param name="type">Type of powerup</param>
    /// <param name="currentSpawned">Current number of those powerups spawned</param>
    /// <returns>Boolean - true if powerup is possible.</returns>
    public bool IsPowerUpPossible(PowerUpType type, int currentSpawned)
    {
        if (type == PowerUpType.Good)
        {
            // if total possible powerups - current active powerups - current spawned
            if (goodPowerUps.Length - numActiveGoodPowerUps - currentSpawned > 0)
            {
                // loop through all powerups
                for (int i = 0; i < goodPowerUps.Length; i++)
                {
                    // if not active
                    if (!goodPowerUps[i].GetIsActive())
                    {
                        // if can be activated
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
