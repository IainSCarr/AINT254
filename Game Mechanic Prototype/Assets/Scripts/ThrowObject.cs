using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowObject : MonoBehaviour {

    private Vector3 startPos;
    private Vector3 endPos;

    private float magnitude;
    private Vector3 throwAngle;

    public GameObject obj;
    private Transform objPos;

    private int numThrows;
    public Text uiThrowText;

	// Use this for initialization
	void Start () {
        objPos = obj.GetComponent<Transform>();

        numThrows = 0;
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

            ThrowBall(startPos, endPos);
        }
	}

    void ThrowBall(Vector3 start, Vector3 end)
    {
        magnitude = Vector3.Magnitude(end - start);
        Debug.Log(magnitude);


        throwAngle = (Vector3.Normalize(start - end) * -1) + transform.forward;
        throwAngle += (Vector3.up * 0.4f);

        SendMessage("ReleaseBall");

        obj.GetComponent<Rigidbody>().AddRelativeForce(throwAngle * magnitude * 100);

        numThrows++;
        uiThrowText.text = "Throws: " + numThrows;
    }
}
