using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class PotBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
        iTween.MoveFrom(gameObject, iTween.Hash("y", 20));
        SetRotation(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SetRotation(true);
        }
	}

    public void SetRotation(bool rotation)
    {
        if (rotation)
        {
            GetComponent<AutoMoveAndRotate>().enabled = true;
        }
        else
        {
            GetComponent<AutoMoveAndRotate>().enabled = false;
        }
    }
}
