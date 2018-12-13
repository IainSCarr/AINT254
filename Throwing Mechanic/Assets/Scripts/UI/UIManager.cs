using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject notification;

    public Slider healthBar;
    public Text timer;

    public GameObject endScreen;
    public GameObject gameInfo;

    public ScoreManager scoreManager;

    public Text finalScore;
    public Text highscore;

    private int health;

    float totalTime = 180.0f;
    private bool isTicking = false;
    private bool hasGameStarted = false;
    private bool hasGameEnded;

    private AudioManager instance;

    void OnEnable()
    {
        PlayerBehaviour.OnUpdateHealth += HandleonUpdateHealth;
    }

    private void OnDisable()
    {
        PlayerBehaviour.OnUpdateHealth -= HandleonUpdateHealth;
    }

    void Start()
    {
        Time.timeScale = 1.0f;


        UpdateLevelTimer(totalTime);

        instance = AudioManager.instance;
    }

    //void Update()
    //{
    //    if (hasGameStarted)
    //    {
    //        totalTime -= Time.deltaTime;

    //        if (totalTime > 0)
    //        {
    //            if (totalTime <= 30)
    //            {
    //                if (!isTicking)
    //                {
    //                    isTicking = true;
    //                    instance.PlaySound("Ticking");
    //                }
    //            }

    //            UpdateLevelTimer(totalTime);
    //        }
    //        else
    //        {
    //            if (!hasGameEnded)
    //            {
    //                hasGameEnded = true;
    //                EndGame();
    //            }
    //        }
    //    }
    //}

    // Code below used from Unity Answers for simple formatting
    private void UpdateLevelTimer(float totalSeconds)
    {
        int minutes = Mathf.FloorToInt(totalSeconds / 60f);
        int seconds = Mathf.RoundToInt(totalSeconds % 60f);

        string formatedSeconds = seconds.ToString();

        if (seconds == 60)
        {
            seconds = 0;
            minutes += 1;
        }

        timer.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    public void ShowNotification(string text, bool goodNotification)
    {
        //notification.SetActive(false);
        //if (goodNotification)
        //{
        //    notification.GetComponent<Text>().color = Color.green;
        //}
        //else
        //{
        //    notification.GetComponent<Text>().color = Color.red;
        //}
        //notification.GetComponent<Text>().text = text;
        //notification.SetActive(true);
    }

    void HandleonUpdateHealth(int newHealth)
    {
        health = newHealth;
        healthBar.value = health;

        if (health <= 0)
        {
            instance.PlaySound("PlayerDeath");
            EndGame();
        }
        else
        {
            instance.PlaySound("HitPlayer");
        }
    }

    private void EndGame()
    {
        Time.timeScale = 0f;
        finalScore.text = "YOUR SCORE: " + scoreManager.GetScore().ToString();

        instance.StopSound("Ticking");
        instance.StopSound("GameMusic");
        instance.PlaySound("GameOver");
        //instance.PlaySound("EndMusic");

        SetHighScore();

        gameInfo.SetActive(false);
        endScreen.SetActive(true);
    }

    private void SetHighScore()
    {
        if (scoreManager.GetScore() > PlayerPrefs.GetInt("Highscore", 0))
        {
            PlayerPrefs.SetInt("Highscore", scoreManager.GetScore());
        }
        highscore.text = "HIGH SCORE:  " + PlayerPrefs.GetInt("Highscore", 0).ToString();
    }


    public void GameStart()
    {
        hasGameStarted = true;
    }
}
