using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePowerUps : MonoBehaviour {

    private EnemyManager enemyManager;
    private ObjectManager objectManager;
    private SecondUIManager uiManager;

    private int numActiveGoodPowerUps;
    private int numActiveBadPowerUps;

    private bool powerupWasActivated;

    private bool[] currentGoodPowerUps = new bool[6];
    private string[] goodPowerUpNames = new string[6] { "INCREASED GRAVITY", "QUICKFIRE ACTIVATED", "X2 SCORE MULTIPLIER", "EXPLODING OBJECTS", "ALL ENEMIES KILLED", "BOUNCY OBJECTS" };

    private bool[] currentBadPowerUps = new bool[2];
    private string[] badPowerUpNames = new string[2] { "BARRAGE INCOMING", "ROTATING TARGETS" };

    public delegate void IncreaseFireRate(float rate);
    public static event IncreaseFireRate OnIncreaseFireRate;

    public delegate void RotateTargets(bool isActive);
    public static event RotateTargets OnRotateTargets;


	// Use this for initialization
	void Start () {
        enemyManager = FindObjectOfType<EnemyManager>();
        objectManager = FindObjectOfType<ObjectManager>();
        uiManager = FindObjectOfType<SecondUIManager>();
    }

    public void DoGoodPowerUp()
    {
        // if not all good powerups are active
        if (numActiveGoodPowerUps < goodPowerUpNames.Length)
        {
            int num = Random.Range(0, goodPowerUpNames.Length);

            // if this powerup isn't active
            if (!currentGoodPowerUps[num])
            {
                powerupWasActivated = true;

                // Find and do powerup
                switch (num)
                {
                    case 0:
                        // increase gravity
                        Physics.gravity = new Vector3(0, -40, 0);
                        break;
                    case 1:
                        // increase fire rate
                        objectManager.SetPlayerFireRate(0.5f);
                        break;
                    case 2:
                        // increase score multiplier
                        uiManager.SetDoubledScore(true);
                        break;
                    case 3:
                        // set explodable objects
                        objectManager.SetExplodableObjects(true);
                        break;
                    case 4:
                        // destroy all enemies
                        // if there are enemies in the scene
                        if (enemyManager.GetCurrentEnemies() > 0)
                        {
                            enemyManager.DestroyAllEnemies();
                        }
                        else
                        {
                            powerupWasActivated = false;
                        }
                        break;
                    case 5:
                        objectManager.SetBouncyObjects(true);
                        break;
                    default:
                        break;
                }

                if (powerupWasActivated)
                {
                    currentGoodPowerUps[num] = true;
                    numActiveGoodPowerUps++;
                    uiManager.ShowNotification(goodPowerUpNames[num], true);
                    StartCoroutine(StartDeactivation(num, true));
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
        // if not all bad powerups are active
        if (numActiveBadPowerUps < badPowerUpNames.Length)
        {
            int num = Random.Range(0, badPowerUpNames.Length);

            // if this powerup isn't active
            if (!currentBadPowerUps[num])
            {
                powerupWasActivated = true;

                // find and do powerup
                switch (num)
                {
                    case 0:
                        if (enemyManager.GetCurrentEnemies() > 0)
                        {
                            if (OnIncreaseFireRate != null)
                            {
                                OnIncreaseFireRate(0.25f);
                            }
                        }
                        else
                        {
                            powerupWasActivated = false;
                        }
                        break;
                    case 1:
                        if (OnRotateTargets != null)
                        {
                            OnRotateTargets(true);
                        }
                        else
                        {
                            powerupWasActivated = false;
                        }
                        break;
                    default:
                        break;
                }


                if (powerupWasActivated)
                {
                    numActiveBadPowerUps++;
                    currentBadPowerUps[num] = true;
                    uiManager.ShowNotification(badPowerUpNames[num], false);
                    StartCoroutine(StartDeactivation(num, false));
                }
            }
            else
            {
                DoBadPowerUp();
            }
        }
    }

    IEnumerator StartDeactivation(int num, bool type)
    {
        yield return new WaitForSeconds(30f);
        if (type)
        {
            DeactivateGoodPowerUp(num);
        }
        else
        {
            DeactivateBadPowerUp(num);
        }
    }

    private void DeactivateGoodPowerUp(int num)
    {
        switch (num)
        {
            case 0:
                Physics.gravity = new Vector3(0, -20, 0);
                break;
            case 1:
                objectManager.SetPlayerFireRate(2f);
                break;
            case 2:
                // Decrease score multiplier
                uiManager.SetDoubledScore(false);
                break;
            case 3:
                objectManager.SetExplodableObjects(false);
                break;
            case 5:
                objectManager.SetBouncyObjects(false);
                break;
            default:
                break;
        }

        currentGoodPowerUps[num] = false;
        numActiveGoodPowerUps--;
        Debug.Log(goodPowerUpNames[num] + " deactivated");
    }

    private void DeactivateBadPowerUp(int num)
    {
        switch (num)
        {
            case 1:
                if (OnRotateTargets != null)
                {
                    OnRotateTargets(false);
                }
                break;
            default:
                break;
        }

        currentBadPowerUps[num] = false;
        numActiveBadPowerUps--;
        Debug.Log(badPowerUpNames[num] + " deactivated");
    }
}
