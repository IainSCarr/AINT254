using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    private int score;
    private Text scoreText;

    private bool isScoreDoubled;

    void OnEnable()
    {
        AddScore.OnSendScore += HandleonSendScore;
    }

    private void OnDisable()
    {
        AddScore.OnSendScore -= HandleonSendScore;
    }

    // Use this for initialization
    void Start () {
        scoreText = GetComponent<Text>();

        score = 0;
        scoreText.text = score.ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void HandleonSendScore(int theScore)
    {
        if (isScoreDoubled)
        {
            score += (theScore * 2);
        }
        else
        {
            score += theScore;
        }

        scoreText.text = score.ToString();

        AnimateIn();
    }

    private void AnimateIn()
    {
        iTween.ScaleBy(gameObject, iTween.Hash("x", 1.5, "y", 1.5, "z", 1.5, "time", 0.5, "easetype", iTween.EaseType.easeOutElastic, "oncomplete", "AnimateOut"));
        iTween.ColorTo(gameObject, iTween.Hash("color", Color.red));
    }

    private void AnimateOut()
    {
        iTween.ScaleTo(gameObject, iTween.Hash("x", 1, "y", 1, "z", 1, "time", 0.5, "easetype", iTween.EaseType.easeInOutElastic));
        iTween.ColorTo(gameObject, iTween.Hash("color", Color.white));
    }

    public int GetScore()
    {
        return score;
    }

    public void SetDoubledScore(bool setting)
    {
        isScoreDoubled = setting;
    }
}
