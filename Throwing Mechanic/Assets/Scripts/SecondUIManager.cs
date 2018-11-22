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

    private int health;
    private int score;
    private int highscore;

    float totalTime = 10.0f;

    void OnEnable()
    {
        PlayerBehaviour.OnUpdateHealth += HandleonUpdateHealth;
        //AddScore.OnSendScore += HandleonSendScore;
    }

    private void Start()
    {
        Time.timeScale = 1.0f;
        score = 0;
        scoreText.text = score.ToString();
    }

    void Update()
    {
        totalTime -= Time.deltaTime;

        if (totalTime <= 0)
        {
            gameInfo.SetActive(false);
            Time.timeScale = 0f;
            finalScore.text = score.ToString();
            endScreen.SetActive(true);
        }
        else
        {
            UpdateLevelTimer(totalTime);
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
    }

    void HandleonSendScore(int theScore)
    {
        score += theScore;
        scoreText.text = score.ToString();
    }


}
