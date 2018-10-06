using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldObject : MonoBehaviour {

    private Transform pos;
    public GameObject obj;
    private Transform objPos;

	// Use this for initialization
	void Start () {
        pos = transform;
        objPos = obj.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        objPos.position = pos.position + pos.forward * 10;
	}
}
