using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldObject : MonoBehaviour {

    private Transform pos;
    private GameObject[] objArray;
    private Transform objPos;

    public GameObject cube;
    public GameObject sphere;
    public GameObject capsule;

    private int currentObj = 0;

    private bool holdingObj;

    private void Awake()
    {
        objArray = new GameObject[3] { cube, sphere, capsule };
    }

    // Use this for initialization
    void Start () {
        currentObj = 0;
        pos = transform;
        objPos = objArray[currentObj].GetComponent<Transform>();
        holdingObj = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (holdingObj)
        {
            objPos.position = pos.position + (pos.forward * 10) + (pos.up * -3);
        }

        if (!holdingObj && Input.GetKeyDown(KeyCode.Space))
        {
            ResetPosition();
        }
    }

    void ReleaseBall()
    {
        holdingObj = false;

        if (currentObj == 2)
        {
            currentObj = 0;
        }
        else
        {
            currentObj++;
        }

        StartCoroutine(Example());


    }

    void ResetPosition()
    {
        holdingObj = true;
        objArray[currentObj].GetComponent<Rigidbody>().velocity = Vector3.zero;
        objArray[currentObj].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        objArray[currentObj].GetComponent<Transform>().rotation = Quaternion.identity;
    }

    IEnumerator Example()
    {
        yield return new WaitForSeconds(0.5f);
        objPos = objArray[currentObj].GetComponent<Transform>();
        ResetPosition();
    }
}


