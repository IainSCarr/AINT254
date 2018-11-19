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

    private bool[] currentGoodPowerUps = new bool[5];
    private string[] goodPowerUpNames = new string[5] { "INCREASED GRAVITY", "QUICKFIRE ACTIVATED", "X2 SCORE MULTIPLIER", "EXPLODING OBJECTS", "ALL ENEMIES KILLED" };

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
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DoGoodPowerUp()
    {
        if (numActiveGoodPowerUps < goodPowerUpNames.Length)
        {
            int num = Random.Range(0, goodPowerUpNames.Length);

            if (!currentGoodPowerUps[num])
            {
                powerupWasActivated = true;
                switch (num)
                {
                    case 0:
                        Physics.gravity = new Vector3(0, -40, 0);
                        break;
                    case 1:
                        objectManager.SetPlayerFireRate(0.5f);
                        break;
                    case 2:
                        break;
                    case 3:
                        objectManager.SetExplodableObjects(true);
                        break;
                    case 4:
                        if (enemyManager.GetCurrentEnemies() > 0)
                        {
                            Debug.Log(enemyManager.GetCurrentEnemies());
                            enemyManager.DestroyAllEnemies();
                        }
                        else
                        {
                            powerupWasActivated = false;
                        }
                        break;
                    default:
                        break;
                }

                currentGoodPowerUps[num] = true;
                numActiveGoodPowerUps++;

                if (powerupWasActivated)
                {
                    uiManager.ShowNotification(goodPowerUpNames[num], true);
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
        if (numActiveBadPowerUps < badPowerUpNames.Length)
        {
            int num = Random.Range(0, badPowerUpNames.Length);

            if (!currentBadPowerUps[num])
            {
                switch (num)
                {
                    case 0:
                        if (enemyManager.GetCurrentEnemies() > 0)
                        {
                            if (OnIncreaseFireRate != null)
                            {
                                OnIncreaseFireRate(0.25f);
                                powerupWasActivated = true;
                            }
                        }
                        break;
                    case 1:
                        if (OnRotateTargets != null)
                        {
                            OnRotateTargets(true);
                            powerupWasActivated = true;
                        }
                        break;
                }

                currentBadPowerUps[num] = true;
                numActiveBadPowerUps++;

                if (powerupWasActivated)
                {
                    uiManager.ShowNotification(goodPowerUpNames[num], true);
                    powerupWasActivated = false;
                }
            }
            else
            {
                DoBadPowerUp();
            }
        }
    }

    private void DeactivatePowerUp()
    {

    }
}
