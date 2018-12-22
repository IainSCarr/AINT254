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

        instance = AudioManager.instance;
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
