using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondUIManager : MonoBehaviour {

    public GameObject notification;
    public Slider healthBar;
    public Text scoreText;

    private int health;
    private int score;
    private int highscore;

    void OnEnable()
    {
        PlayerBehaviour.OnUpdateHealth += HandleonUpdateHealth;
        //AddScore.OnSendScore += HandleonSendScore;
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
