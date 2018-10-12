using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowObject : MonoBehaviour {

    private Vector3 startPos;
    private Vector3 endPos;

    private Vector3 throwAngle;

    public GameObject obj;
    private Transform objPos;

    public GameObject dot;
    [SerializeField]
    private GameObject[] dots;

	// Use this for initialization
	void Start () {
        objPos = obj.transform.GetChild(0);

        dots = new GameObject[10];

        for (int i = 0; i < 10; i++)
        {
            GameObject tempDot = Instantiate(dot);

            dots[i] = tempDot;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            DisplayTrajectory();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endPos = Input.mousePosition;

            ThrowBall(startPos, endPos);
        }
	}

    void ThrowBall(Vector3 start, Vector3 end)
    {
        float magnitude = Vector3.Magnitude(end - start);

        throwAngle = (Vector3.Normalize(start - end) * -1) + transform.forward;
        throwAngle += (Vector3.up * 0.4f);

        DisplayTrajectory();

        SendMessage("ReleaseBall");

        objPos.GetComponent<Rigidbody>().AddRelativeForce(throwAngle * magnitude * 100);
    }

    void DisplayTrajectory()
    {
        for (int i = 0; i < 10; i++)
        {
            float time = i * 0.1f;

            float dX = throwAngle.x * time;
            float dY = (throwAngle.y * time) - (0.5f * 20 * time * time);
            float dZ = throwAngle.z * time;

            Vector3 position = new Vector3(dX, dY, dZ);

            dots[i].transform.position = objPos.position + position;
        }

    }
}
