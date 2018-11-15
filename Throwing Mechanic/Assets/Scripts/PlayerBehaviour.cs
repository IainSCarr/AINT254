using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

    private int health = 100;

    private int speed;

    public delegate void IncreaseFireRate(float rate);
    public static event IncreaseFireRate OnIncreaseFireRate;

    private GameObject currentObject;

    // Use this for initialization
    void Start () {
        Debug.Log(currentObject);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (OnIncreaseFireRate != null)
            {
                OnIncreaseFireRate(0.25f);
            }
        }
	}

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            //End game
        }
    }

    public void SetCurrentObject(GameObject obj)
    {
        currentObject = obj;
    }

    public GameObject GetCurrentObject()
    {
        return currentObject;
    }
}
