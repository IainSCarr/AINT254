﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour {

    public GameObject objectPrefab;

    private TrajectoryController trajectoryController;
    private PlayerBehaviour player;

    private bool explodableObjects;

	// Use this for initialization
	void Start () {
        trajectoryController = FindObjectOfType<TrajectoryController>();
         player = FindObjectOfType<PlayerBehaviour>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Spawn();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            Debug.Log("Explodable Objects");
            SetExplodableObjects(true);
        }
	}

    /// <summary>
    /// Spawns a throwable object if the player isn't currently holding one.
    /// </summary>
    private void Spawn()
    {
        if (!player.GetCurrentObject())
        {
            // create object
            GameObject newObject = Instantiate(objectPrefab);

            // set it's properties
            SetObjectProperties(newObject);

            // update trajectory controller
            trajectoryController.SetProjectile(newObject);
        }
    }

    private void SetObjectProperties(GameObject newObject)
    {
        if (explodableObjects)
        {
            newObject.GetComponent<MultiplyExplode>().SetCanExplode(true);
        }
    }

    public void SetExplodableObjects(bool setting)
    {
        explodableObjects = setting;
    }
}