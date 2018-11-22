using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCountdown : MonoBehaviour {

    private Text text;

    private int number;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        number = 6;
        CountDown();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void CountDown()
    {
        number--;

        if (number > 0)
        {
            text.text = number.ToString();
            AnimateIn();
        }
        else
        {
            text.text = "GO!";
            AnimateIn();
        }
    }

    private void AnimateIn()
    {
        iTween.ScaleTo(gameObject, iTween.Hash("scale", new Vector3(1,1,1), "oncomplete", "AnimateOut", "time", 0.5f));
    }

    private void AnimateOut()
    {
        if (number != 0)
        {
            Invoke("CountDown", 0.5f);
        }

        iTween.ScaleTo(gameObject, iTween.Hash("scale", Vector3.zero, "time", 0.5f));

    }
}
