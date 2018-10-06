using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour {

    private Vector3 startPos;
    private Vector3 endPos;

    private float magnitude;
    private Vector3 throwAngle;

    public GameObject obj;
    private Transform objPos;

	// Use this for initialization
	void Start () {
        objPos = obj.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endPos = Input.mousePosition;
            magnitude = Vector3.Magnitude(endPos - startPos);
            Debug.Log(magnitude);


            throwAngle = (Vector3.Normalize(startPos - endPos) * -1) + transform.forward;

            SendMessage("ReleaseBall");

            obj.GetComponent<Rigidbody>().AddRelativeForce(throwAngle * magnitude * 100);
        }
	}
}
