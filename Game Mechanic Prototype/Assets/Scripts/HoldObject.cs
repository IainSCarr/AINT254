using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldObject : MonoBehaviour {

    private Transform pos;

    public GameObject obj;
    private Transform ingredient;

    private bool holdingObj;

	// Use this for initialization
	void Start () {
        pos = transform;

        ingredient = obj.transform.GetChild(0);
        ResetPosition();
        holdingObj = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (holdingObj)
        {
            ingredient.position = pos.position + (pos.forward * 5) + (pos.up * -3);
        }

        if (!holdingObj && Input.GetKeyDown(KeyCode.Space))
        {
            ResetPosition();
        }
    }

    void ReleaseBall()
    {
        holdingObj = false;
    }

    void ResetPosition()
    {
        holdingObj = true;
        ingredient.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ingredient.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        ingredient.GetComponent<Transform>().rotation = Quaternion.identity;
    }
}
