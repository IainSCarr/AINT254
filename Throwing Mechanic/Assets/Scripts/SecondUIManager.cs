using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondUIManager : MonoBehaviour {

    private GameObject notification;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name == "Notification")
            {
                notification = transform.GetChild(i).gameObject;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            notification.SetActive(true);
        }
    }
}
