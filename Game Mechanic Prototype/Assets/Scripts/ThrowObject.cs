using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowObject : MonoBehaviour {

    private Vector3 startPos;
    private Vector3 endPos;

    private float magnitude;
    private Vector3 throwAngle;

    public GameObject[] obj;
    private Transform objPos;

    private int numThrows;
    public Text uiThrowText;

    private int currentObj;

	// Use this for initialization
	void Start () {
        currentObj = 0;

        objPos = obj[currentObj].GetComponent<Transform>();

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

        obj[currentObj].GetComponent<Rigidbody>().AddRelativeForce(throwAngle * magnitude * 100);

        if (currentObj == 2)
        {
            currentObj = 0;
        }
        else
        {
            currentObj++;
        }
    }
}
