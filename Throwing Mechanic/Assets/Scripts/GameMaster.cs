using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    public enum SpawnState { Spawning, Counting, Waiting };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform manager;
        public int count;
        public int rate;
    }

    public Wave[] waves;
    private int nextWave = 0;
    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    public SpawnState state = SpawnState.Counting;

    private AudioManager instance;

    private EnemyManager enemyManager;
    private ObstacleManager obstacleManager;
    private TargetManager targetManager;
    private NewPowerUpManager powerUpManager;

    private float enemySpawnRate = 11f;
    private float obstacleSpawnRate = 25f;
    private float targetSpawnRate = 9f;
    private float powerupSpawnRate = 13f;

    private void Awake()
    {
        if (AudioManager.instance == null)
        {
            instance = new AudioManager();
        }
    }

    // Use this for initialization
    void Start () {
        enemyManager = FindObjectOfType<EnemyManager>();
        obstacleManager = FindObjectOfType<ObstacleManager>();
        targetManager = FindObjectOfType<TargetManager>();
        powerUpManager = FindObjectOfType<NewPowerUpManager>();

        instance = AudioManager.instance;

        Invoke("StartGame", 5f);

        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {
        if (waveCountdown <= 0)
        {
            if (state != SpawnState.Spawning)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    IEnumerator SpawnWave (Wave _wave)
    {
        state = SpawnState.Spawning;

        for (int i = 0; i < _wave.count; i++)
        {
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.Waiting;

        yield break;
    }

    private void StartGame()
    {
        instance.PlaySound("GameMusic");

        InvokeRepeating("SpawnEnemy", 5f, enemySpawnRate);
        InvokeRepeating("SpawnTarget", 0f, targetSpawnRate);
        InvokeRepeating("SpawnObstacle", 15f, obstacleSpawnRate);
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
