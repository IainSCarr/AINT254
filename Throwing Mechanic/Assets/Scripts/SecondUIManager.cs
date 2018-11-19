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
    }

    public void ShowNotification(string text, bool goodNotification)
    {
        notification.SetActive(false);
        if (goodNotification)
        {
            notification.GetComponent<Text>().color = Color.green;
        }
        else
        {
            notification.GetComponent<Text>().color = Color.red;
        }
        notification.GetComponent<Text>().text = text;
        notification.SetActive(true);
    }
}
