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
    private BarragePowerUp barrage;

    private PowerUp[] goodPowerUps;
    private PowerUp[] badPowerUps;
    private PowerUp[] randomPowerUps;




    private int numActiveGoodPowerUps;
    private int numActiveBadPowerUps;
    private int numActiveRandomPowerUps;

    private bool powerupWasActivated;

    // Use this for initialization
    void Start () {
        ActivatePowerUps reference = gameObject.GetComponent<ActivatePowerUps>();

        // good powerups
        scoreMultiplier = holder.AddComponent<ScorePowerUp>();
        killEnemies = holder.AddComponent<KillEnemiesPowerUp>();
        quickfire = holder.AddComponent<QuickfirePowerUp>();

        //bad powerups
        barrage = holder.AddComponent<BarragePowerUp>();

        // random powerups
        explode = holder.AddComponent<ExplodePowerUp>();
        bounce = holder.AddComponent<BouncyPowerUp>();
        bigObjects = holder.AddComponent<BigObjectsPowerUp>();

        goodPowerUps = new PowerUp[2] { scoreMultiplier, killEnemies };
        for (int i = 0; i < goodPowerUps.Length; i++)
        {
            goodPowerUps[i].SetManager(reference);
        }

        badPowerUps = new PowerUp[1] { barrage };
        for (int i = 0; i < badPowerUps.Length; i++)
        {
            badPowerUps[i].SetManager(reference);
        }

        randomPowerUps = new PowerUp[3] { explode, bounce, bigObjects };
        for (int i = 0; i < randomPowerUps.Length; i++)
        {
            randomPowerUps[i].SetManager(reference);
        }
    }

    public void DoGoodPowerUp()
    {
        if (numActiveGoodPowerUps < goodPowerUps.Length)
        {
            int num = Random.Range(0, goodPowerUps.Length);

            if (!goodPowerUps[num].GetIsActive())
            {
                if (goodPowerUps[num].GetIsPossible())
                {
                    goodPowerUps[num].Enable();

                    numActiveGoodPowerUps++;
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

    public void PowerUpDisabled(PowerUpType type)
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

    public bool IsPowerUpPossible(PowerUpType type)
    {
        if (type == PowerUpType.Good)
        {
            for (int i = 0; i < goodPowerUps.Length; i++)
            {
                if (!goodPowerUps[i].GetIsActive())
                {
                    if (goodPowerUps[i].GetIsPossible())
                    {
                        return true;
                    }
                }
            }

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
                        return true;
                    }
                }
            }

            return false;
        }
        else
        {
            for (int i = 0; i < randomPowerUps.Length; i++)
            {
                if (!randomPowerUps[i].GetIsActive())
                {
                    if (randomPowerUps[i].GetIsPossible())
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
