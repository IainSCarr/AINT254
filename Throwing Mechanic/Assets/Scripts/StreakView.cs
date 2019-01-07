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
        // save initial position
        initialPosition = feedback.transform.position;
	}

    /// <summary>
    /// Updates the streak text and animates it
    /// </summary>
    /// <param name="num"></param>
    public void UpdateStreak(int num)
    {
        streakText.text = num.ToString();

        iTween.ScaleBy(streakText.gameObject, iTween.Hash("amount", scaleAmount, "looptype", iTween.LoopType.pingPong));

        if (num == 3)
        {
            GoodStreak();
        }
        else if (num == 5)
        {
            GreatStreak();
        }
        else if (num == 10)
        {
            AmazingStreak();
        }
    }

    /// <summary>
    /// Displays congratulations message to user
    /// </summary>
    private void GoodStreak()
    {
        feedback.text = "NICE!";
        AnimateText();
        AudioManager.instance.PlaySound("Cheer");
    }

    /// <summary>
    /// Displays congratulations message to user
    /// </summary>
    private void GreatStreak()
    {
        feedback.text = "GREAT!";
        AnimateText();
        AudioManager.instance.PlaySound("Cheer");
    }

    /// <summary>
    /// Displays congratulations message to user
    /// </summary>
    private void AmazingStreak()
    {
        feedback.text = "AMAZING!";
        AnimateText();
        AudioManager.instance.PlaySound("Cheer");
    }

    /// <summary>
    /// Animates congratulatory message to user
    /// </summary>
    private void AnimateText()
    {
        feedback.gameObject.SetActive(true);
        iTween.MoveAdd(feedback.gameObject, iTween.Hash("amount", moveAmount, "time", 1f, "easytype", iTween.EaseType.easeOutBack, "oncompletetarget", gameObject, "oncomplete", "ResetText"));
    }

    /// <summary>
    /// Resets message gameobject to original posistion
    /// </summary>
    public void ResetText()
    {
        feedback.gameObject.SetActive(false);
        feedback.transform.position = initialPosition;
    }
}
