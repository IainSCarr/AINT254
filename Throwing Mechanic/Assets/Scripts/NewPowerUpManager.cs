using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPowerUpManager : MonoBehaviour {

    public GameObject[] powerUpPrefabs;

    private Transform[] spawnArray;
    private bool[] activeSpawns;

    private int numSpawns;
    private int numPowerUps;

    private EnemyManager enemyManager;
    private ObjectManager objectManager;
    private SecondUIManager uiManager;

    private bool[] currentGoodPowerUps = new bool[5];
    private string[] goodPowerUpNames = new string[5] {"INCREASED GRAVITY", "QUICKFIRE ACTIVATED", "X2 SCORE MULTIPLIER", "EXPLODING OBJECTS", "ALL ENEMIES KILLED" };

    private bool[] currentBadPowerUps = new bool[2];
    private string[] badPowerUpNames = new string[2] { "BARRAGE INCOMING", "ROTATING TARGETS"};

    public delegate void IncreaseFireRate(float rate);
    public static event IncreaseFireRate OnIncreaseFireRate;

    public delegate void RotateTargets(bool isActive);
    public static event RotateTargets OnRotateTargets;

    // Use this for initialization
    void Start () {
        numSpawns = transform.childCount;

        spawnArray = new Transform[numSpawns];
        activeSpawns = new bool[numSpawns];

        // Loop through all children and create an array of them
        for (int i = 0; i < numSpawns; i++)
        {
            spawnArray[i] = transform.GetChild(i);
        }

        numPowerUps = 0;

        enemyManager = FindObjectOfType<EnemyManager>();
        objectManager = FindObjectOfType<ObjectManager>();
        uiManager = FindObjectOfType<SecondUIManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.V))
        {
            SpawnRandomPowerUp(Random.Range(0, 2));
        }
    }

    private void SpawnRandomPowerUp(int prefabChoice)
    {
        // if spawns are available
        if (numPowerUps < numSpawns)
        {
            int rand = Random.Range(0, numSpawns);

            // if random spawn isn't already taken
            if (!activeSpawns[rand])
            {
                activeSpawns[rand] = true;
                numPowerUps++;
                spawnArray[rand].SendMessage("Spawn", powerUpPrefabs[prefabChoice]);
            }
            else // try again
            {
                SpawnRandomPowerUp(prefabChoice);
            }
        }
        else
        {
            Debug.Log("No spawn points available.");
        }
    }

    public void PowerUpDestroyed(Transform parent)
    {
        numPowerUps--;

        // Loop through spawners and find parent of destroyed powerup
        for (int i = 0; i < numSpawns; i++)
        {
            if (spawnArray[i] == parent)
            {
                // Make spawn available for next obstacle
                activeSpawns[i] = false;
            }
        }
    }

    public void DoGoodPowerUp()
    {
        Debug.Log("Good PowerUp");

        int num = Random.Range(0, goodPowerUpNames.Length);

        if (!currentGoodPowerUps[num])
        {
            switch (num)
            {
                case 0:
                    Physics.gravity = new Vector3(0, -30, 0);
                    break;
                case 1:

                    break;
                case 2:
                    break;
                case 3:
                    objectManager.SetExplodableObjects(true);
                    break;
                case 4:
                    if (enemyManager.GetCurrentEnemies() > 0)
                    {
                        enemyManager.DestroyAllEnemies();
                    }
                    break;
                default:
                    break;
            }

            currentGoodPowerUps[num] = true;
            uiManager.ShowNotification(goodPowerUpNames[num], true);

        }
        else
        {
            DoGoodPowerUp();
        }
    }

    public void DoBadPowerUp()
    {
        Debug.Log("Bad PowerUp");

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
                        }
                    }
                    break;
                case 1:
                    if (OnRotateTargets != null)
                    {
                        OnRotateTargets(true);
                    }
                    break;
            }

            currentGoodPowerUps[num] = true;
            uiManager.ShowNotification(goodPowerUpNames[num], false);

        }
        else
        {
            DoGoodPowerUp();
        }
    }

    private void DeactivatePowerUp()
    {

    }
}
