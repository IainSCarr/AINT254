using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    public enum SpawnState { Spawning, Counting, Waiting };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Spawner[] spawners;
        private int numRequiredSpawners;

        public void SetRequiredSpawners()
        {
            for (int i = 0; i < spawners.Length; i++)
            {
                if (spawners[i].isRequired)
                {
                    numRequiredSpawners++;
                }
            }
        }

        public int GetRequiredSpawners()
        {
            return numRequiredSpawners;
        }
    }

    [System.Serializable]
    public class Spawner
    {
        public SpawnManager manager;
        public int count;
        public int rate;
        public bool isRequired;
    }

    public Wave[] waves;
    private int currentWave = 0;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float searchCountdown = 1f;

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

        //Invoke("StartGame", 5f);

        waveCountdown = timeBetweenWaves;

        // loop through all waves and set property
        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].SetRequiredSpawners();
        }
    }

    void Update()
    {
        if (state == SpawnState.Waiting)
        {
            if (WaveIsOver())
            {
                Debug.Log("Wave completed");
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.Spawning)
            {
                StartCoroutine(SpawnWave(waves[currentWave]));
                Debug.Log("Test");
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    private bool WaveIsOver()
    {
        searchCountdown -= Time.deltaTime;

        if (searchCountdown <= 0)
        {
            searchCountdown = 1f;

            int completedSpawners = 0;

            // Loop through all spawn managers in wave
            for (int i = 0; i < waves[currentWave].spawners.Length; i++)
            {
                // if manager is spawning an object that needs to be destroyed
                if (waves[currentWave].spawners[i].isRequired)
                {
                    // if all objects for that manager have been destroyed
                    if (waves[currentWave].spawners[i].manager.GetNumberOfObjects() == 0)
                    {
                        completedSpawners++;
                    }
                }

                if (waves[currentWave].GetRequiredSpawners() == completedSpawners)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void WaveCompleted()
    {
        Debug.Log("Wave Completed");


        state = SpawnState.Counting;
        waveCountdown = timeBetweenWaves;

        if (currentWave + 1 > waves.Length - 1)
        {
            currentWave = 0;
            Debug.Log("All waves complete.");
        }
        else
        {
            currentWave++;
        }
    }

    IEnumerator SpawnWave (Wave _wave)
    {
        state = SpawnState.Spawning;

        Debug.Log(_wave.spawners.Length);

        for (int i = 0; i < _wave.spawners.Length; i++)
        {
            for (int j = 0; j < _wave.spawners[i].count; j++)
            {
                _wave.spawners[i].manager.SpawnRandom();
                yield return new WaitForSeconds(1f / _wave.spawners[i].rate);
            }
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
        enemyManager.SpawnRandom();
    }

    private void SpawnTarget()
    {
        targetManager.SpawnRandom();
    }

    private void SpawnObstacle()
    {
        obstacleManager.SpawnRandom();
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
