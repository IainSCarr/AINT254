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
        public bool hasEvent;
        public WaveEvent waveEvent;

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
        public string name;
        public SpawnManager manager;
        public int count;
        public float rate;
        public bool isRequired;
    }

    [System.Serializable]
    public class WaveEvent
    {
        public enum EventType {FastEnemies, Barrage, RotatingTargets};
        public EventType eventType;
        public float waitTime;
    }

    public Wave[] waves;
    private int currentWave = 0;

    public float timeBetweenWaves = 5f;
    private float waveCountdown;

    private float searchCountdown = 1f;

    public SpawnState state = SpawnState.Counting;

    private AudioManager instance;
    private ActivatePowerUps powerupActivator;

    private void Awake()
    {
        if (AudioManager.instance == null)
        {
            instance = new AudioManager();
        }
    }

    // Use this for initialization
    void Start () {
        instance = AudioManager.instance;
        powerupActivator = FindObjectOfType<ActivatePowerUps>();

        Invoke("StartGame", 5f);

        waveCountdown = 5f;

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
                SpawnWave(waves[currentWave]);
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

    private void SpawnWave(Wave _wave)
    {
        state = SpawnState.Spawning;

        if (_wave.hasEvent)
        {
            StartCoroutine(StartEvent(_wave.waveEvent));
        }

        // loop through spawners and make them start spawning
        for (int i = 0; i < _wave.spawners.Length; i++)
        {
            StartCoroutine(SpawnObjects(_wave.spawners[i]));
        }
    }

    IEnumerator SpawnObjects(Spawner spawner)
    {
        for (int i = 0; i < spawner.count; i++)
        {
            spawner.manager.SpawnRandom();
            yield return new WaitForSeconds(1f / spawner.rate);
        }

        state = SpawnState.Waiting;

        yield break;
    }

    IEnumerator StartEvent(WaveEvent _event)
    {
        yield return new WaitForSeconds(_event.waitTime);

        if (_event.eventType == WaveEvent.EventType.Barrage)
        {
            powerupActivator.barrage.Enable();
        }
        else if (_event.eventType == WaveEvent.EventType.FastEnemies)
        {
            powerupActivator.fastEnemies.Enable();
        }
        else if (_event.eventType == WaveEvent.EventType.RotatingTargets)
        {
            powerupActivator.moveTargets.Enable();
        }

        yield break;
    }

    private void StartGame()
    {
        instance.PlaySound("GameMusic");
    }
}
