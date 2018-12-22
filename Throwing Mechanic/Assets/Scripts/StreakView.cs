using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StreakView : MonoBehaviour {

    public Text streakText;
    public Text feedback;

    private Vector3 initialPosition;

    private Vector3 scaleAmount = new Vector3(1.5f, 1.5f, 1.5f);
    private Vector3 moveAmount = new Vector3(0, -10f);

	// Use this for initialization
	void Start () {
        initialPosition = feedback.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateStreak(int num)
    {
        streakText.text = num.ToString();

        iTween.ScaleBy(streakText.gameObject, iTween.Hash("amount", scaleAmount, "looptype", iTween.LoopType.pingPong));

        if (num == 2)
        {
            GoodStreak();
        }
        else if (num == 3)
        {
            GreatStreak();
        }
    }

    private void GoodStreak()
    {
        feedback.text = "NICE!";
        AnimateText();
    }

    private void GreatStreak()
    {
        feedback.text = "GREAT!";
        AnimateText();
    }

    private void AmazingStreak()
    {
        feedback.text = "AMAZING!";
        AnimateText();
    }

    private void AnimateText()
    {
        feedback.gameObject.SetActive(true);
        iTween.MoveAdd(feedback.gameObject, iTween.Hash("amount", moveAmount, "time", 1f, "easytype", iTween.EaseType.easeOutBack, "oncompletetarget", gameObject, "oncomplete", "ResetText"));
    }

    public void ResetText()
    {
        Debug.Log("Testttttttttt");
        feedback.gameObject.SetActive(false);
        feedback.transform.position = initialPosition;
    }
}
