using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondUIManager : MonoBehaviour {

    public GameObject notification;

    public Slider healthBar;
    public Text timer;
    public Text scoreText;

    public GameObject endScreen;
    public GameObject gameInfo;

    public Text finalScore;
    public Text highscore;

    private int health;
    private int score;
    private bool isScoreDoubled;

    float totalTime = 180.0f;
    private bool isTicking = false;
    private bool hasGameStarted = false;
    private bool hasGameEnded;

    private AudioManager instance;

    void OnEnable()
    {
        PlayerBehaviour.OnUpdateHealth += HandleonUpdateHealth;
        AddScore.OnSendScore += HandleonSendScore;
    }

    private void OnDisable()
    {
        PlayerBehaviour.OnUpdateHealth -= HandleonUpdateHealth;
        AddScore.OnSendScore -= HandleonSendScore;
    }

    void Start()
    {
        Time.timeScale = 1.0f;

        score = 0;
        scoreText.text = score.ToString();
        UpdateLevelTimer(totalTime);

        instance = AudioManager.instance;
    }

    void Update()
    {
        if (hasGameStarted)
        {
            totalTime -= Time.deltaTime;

            if (totalTime > 0)
            {
                if (totalTime <= 30)
                {
                    if (!isTicking)
                    {
                        isTicking = true;
                        instance.PlaySound("Ticking");
                    }
                }

                UpdateLevelTimer(totalTime);
            }
            else
            {
                if (!hasGameEnded)
                {
                    hasGameEnded = true;
                    EndGame();
                }
            }
        }
    }

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
    }

    private void EndGame()
    {
        Time.timeScale = 0f;
        finalScore.text = "YOUR SCORE: " + score.ToString();

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
        if (score > PlayerPrefs.GetInt("Highscore", 0))
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
        highscore.text = "HIGH SCORE:  " + PlayerPrefs.GetInt("Highscore", 0).ToString();
    }

    public void SetDoubledScore(bool setting)
    {
        isScoreDoubled = setting;
    }

    public void GameStart()
    {
        hasGameStarted = true;
    }
}
