using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StreakController : MonoBehaviour {

    public StreakView view;

    private int numThrows;
    private int numObjectsHit;

    private int currentStreak;

	// Use this for initialization
	void Start () {
        currentStreak = 0;
        numObjectsHit = 0;
        numThrows = 0;
	}

    public void IncreaseStreak()
    {
        numObjectsHit++;

        if (numObjectsHit >= numThrows)
        {
            currentStreak++;
        }
        else
        {
            ResetStreak();
            currentStreak++;
        }

        view.UpdateStreak(currentStreak);
    }

    public void ResetStreak()
    {
        Debug.Log("Resetting Streak");
        numThrows = 0;
        numObjectsHit = 0;
        currentStreak = 0;
        view.UpdateStreak(currentStreak);
    }

    public void IncreaseThrows()
    {
        numThrows++;
    }
}
