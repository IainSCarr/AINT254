using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldObject : MonoBehaviour {

    private Transform pos;
    public GameObject obj;
    private Transform objPos;

    private bool holdingObj;

	// Use this for initialization
	void Start () {
        pos = transform;
        objPos = obj.GetComponent<Transform>();
        holdingObj = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (holdingObj)
        {
            objPos.position = pos.position + pos.forward * 10;
        }

        if (!holdingObj && Input.GetKeyDown(KeyCode.Space))
        {
            obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
            obj.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            obj.GetComponent<Transform>().rotation = Quaternion.identity;
            holdingObj = true;
        }
    }

    void ReleaseBall()
    {
        holdingObj = false;
    }


}
