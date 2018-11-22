using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    private AudioManager instance;

    private EnemyManager enemyManager;
    private ObstacleManager obstacleManager;
    private TargetManager targetManager;
    private NewPowerUpManager powerUpManager;

    private float enemySpawnRate = 10f;
    private float obstacleSpawnRate = 25f;
    private float targetSpawnRate = 10f;
    private float powerupSpawnRate = 20f;

	// Use this for initialization
	void Start () {
        enemyManager = FindObjectOfType<EnemyManager>();
        obstacleManager = FindObjectOfType<ObstacleManager>();
        targetManager = FindObjectOfType<TargetManager>();
        powerUpManager = FindObjectOfType<NewPowerUpManager>();

        instance = AudioManager.instance;

        Invoke("StartGame", 5f);
	}

    private void StartGame()
    {
        instance.PlaySound("GameMusic");

        InvokeRepeating("SpawnEnemy", 5f, enemySpawnRate);
        InvokeRepeating("SpawnTarget", 0f, targetSpawnRate);
        InvokeRepeating("SpawnObstacle", 15f, obstacleSpawnRate);
        InvokeRepeating("SpawnPowerUp", 0f, powerupSpawnRate);
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    private void SpawnEnemy()
    {
        enemyManager.SpawnRandomEnemy();
    }

    private void SpawnTarget()
    {
        targetManager.SpawnRandomTarget();
    }

    private void SpawnObstacle()
    {
        obstacleManager.SpawnRandomObstacle();
    }

    private void SpawnPowerUp()
    {
        powerUpManager.SpawnRandomPowerUp(Random.Range(0, 2));
    }

    public void SetEnemySpawnRate(float rate)
    {
        enemySpawnRate = rate;
    }

    public void SetObstacleSpawnRate(float rate)
    {
        obstacleSpawnRate = rate;
    }

    public void SetTargetSpawnRate(float rate)
    {
        targetSpawnRate = rate;
    }

    public void SetPowerUpSpawnRate(float rate)
    {
        powerupSpawnRate = rate;
    }
}
