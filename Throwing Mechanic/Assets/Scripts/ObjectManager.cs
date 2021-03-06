﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnState { Spawning, Spawned }

public class ObjectManager : MonoBehaviour {

    private SpawnState state;

    public GameObject[] objectPrefab;
    public StreakController streak;

    private TrajectoryController trajectoryController;
    private PlayerBehaviour player;

    private float fireRate = 2f;

    private bool explodableObjects;

    public PhysicMaterial bouncyMaterial;
    private bool bouncyObjects;

    private bool bigObjects;

	// Use this for initialization
	void Start () {
        trajectoryController = FindObjectOfType<TrajectoryController>();
        player = FindObjectOfType<PlayerBehaviour>();

        state = SpawnState.Spawning;
        Invoke("Spawn", 5f);
    }
	
	// Update is called once per frame
	void Update () {

	}

    /// <summary>
    /// Spawns a throwable object if the player isn't currently holding one.
    /// </summary>
    private void Spawn()
    {
        if (!player.GetCurrentObject())
        {
            // create object
            GameObject newObject = Instantiate(objectPrefab[player.GetSelectedObject()], player.transform.position - (player.transform.forward * 2), player.transform.rotation, gameObject.transform);
            
            // set it's properties
            SetObjectProperties(newObject);

            // update trajectory controller
            trajectoryController.SetProjectile(newObject);

            state = SpawnState.Spawned;
        }
    }

    public void SwitchObject()
    {
        if (state != SpawnState.Spawning)
        {
            player.DestroyObject();
            Spawn();
        }

        // if is not waiting for spawn
        // destroy current object
        // spawn new object
    }

    public void StartSpawn()
    {
        state = SpawnState.Spawning;
        Invoke("Spawn", fireRate);
    }

    private void SetObjectProperties(GameObject newObject)
    {
        if (explodableObjects)
        {
            newObject.GetComponent<MultiplyExplode>().SetCanExplode(true);
        }

        if (bouncyObjects)
        {
            newObject.GetComponent<Collider>().material = bouncyMaterial;
        }

        if (bigObjects)
        {
            newObject.GetComponent<Transform>().localScale *= 2;
        }
    }

    public void SetExplodableObjects(bool setting)
    {
        explodableObjects = setting;
    }

    public void SetBouncyObjects(bool setting)
    {
        bouncyObjects = setting;
    }

    public void SetPlayerFireRate(float rate)
    {
        fireRate = rate;
    }

    public void SetBigObjects(bool setting)
    {
        bigObjects = setting;
    }

    public void NoObjectHit()
    {
        Debug.Log("NoObjectHit");
        streak.ResetStreak();
    }

    public int GetNumObjects()
    {
        return objectPrefab.Length;
    }

    public GameObject[] GetObjects()
    {
        return objectPrefab;
    }
}
