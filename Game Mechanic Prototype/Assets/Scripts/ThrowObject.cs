using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour {

    private float startTime;
    private float endTime;
    private float throwPower;

    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 throwAngle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            startTime = Time.time;
            startPos = Input.mousePosition;
            Debug.Log(startPos);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endTime = Time.time;
            throwPower = endTime - startTime;

            endPos = Input.mousePosition;
            Debug.Log(endPos);
            throwAngle = Vector3.Normalize(endPos - startPos) + transform.forward;
            Debug.Log(throwAngle);
        }
	}
}
