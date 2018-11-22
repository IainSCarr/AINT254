using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//public class GameUI : MonoBehaviour {

//    public Slider healthBar;
//    public Text scoreText;
//    public Text finalScoreText;
//    public Text highscoreText;

//    private int health;
//    private int score;
//    private int highscore;

//    void OnEnable()
//    {
//        PlayerBehaviour.OnUpdateHealth += HandleonUpdateHealth;
//        PlayerBehaviour.OnPlayerDeath += HandleonPlayerDeath;
//        AddScore.OnSendScore += HandleonSendScore;
//    }

//    void OnDisable()
//    {
//        PlayerBehaviour.OnUpdateHealth -= HandleonUpdateHealth;
//        PlayerBehaviour.OnPlayerDeath -= HandleonPlayerDeath;
//        AddScore.OnSendScore -= HandleonSendScore;
//    }

//    void HandleonPlayerDeath()
//    {
//        finalScoreText.text = "YOUR  SCORE:  " + score.ToString();
//        if (score > PlayerPrefs.GetInt("Highscore", 0))
//        {
//            PlayerPrefs.SetInt("Highscore", score);
//        }
//        highscoreText.text = "HIGHSCORE:  " + PlayerPrefs.GetInt("Highscore", 0).ToString();
//    }

//    void HandleonUpdateHealth(int newHealth)
//    {
//        health = newHealth;
//        healthBar.value = health;
//    }

//    void HandleonSendScore(int theScore)
//    {
//        score += theScore;
//        scoreText.text = score.ToString();
//    }

//    public void ResetGame()
//    {
//        score = 0;
//        health = 100;
//        scoreText.text = score.ToString();
//    }
//}
