using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowObject : MonoBehaviour {

    public Transform target;

    private Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = new Vector3(0, -55);
	}
	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 wantedPos = Camera.main.WorldToScreenPoint(target.position);
        transform.position = wantedPos + offset;
    }
}
