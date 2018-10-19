using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldObject : MonoBehaviour {

    private Transform playerPosition;
    public GameObject currentObject;
    private Transform objectPosition;

    private bool holdingObj;

    private void Awake()
    {
    }

    // Use this for initialization
    void Start () {
        playerPosition = GetComponent<Transform>();

        objectPosition = currentObject.GetComponent<Transform>();
        objectPosition.position = playerPosition.position + (playerPosition.forward * 2) + (playerPosition.up * -2);
        ResetPosition();
        holdingObj = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (holdingObj)
        {
            objectPosition.position = playerPosition.position + (playerPosition.forward * 6) - (playerPosition.up);
        }

        if (!holdingObj && Input.GetKeyDown(KeyCode.Space))
        {
            ResetPosition();
            currentObject.SendMessage("Reset");
        }
    }

    void ReleaseBall()
    {
        holdingObj = false;
    }

    void ResetPosition()
    {
        holdingObj = true;
        currentObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        currentObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        currentObject.GetComponent<Transform>().rotation = Quaternion.identity;
    }
}


