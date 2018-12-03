using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowObject : MonoBehaviour {

    public Transform target;

    [Range(-10.0f, 10.0f)]
    public float heightOffset;

    [Range(-10.0f, 10.0f)]
    public float widthOffset;

    private Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = new Vector3(widthOffset, heightOffset);
	}
	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 wantedPos = Camera.main.WorldToScreenPoint(target.position);
        Debug.Log(wantedPos);
        Debug.Log("TEST");
        transform.position = wantedPos;
    }
}
