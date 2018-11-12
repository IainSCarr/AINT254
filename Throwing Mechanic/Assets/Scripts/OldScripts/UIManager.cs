using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Text powerup;
    public Text timer;
    public Text score;

    public GameObject endScreen;
    public GameObject gameInfo;
    public Text finalScore;

    private int points;

    float totalTime = 10.0f;

    void OnEnable()
    {
        Target.OnSendScore += HandleOnSendScore;
        PotCollider.OnSendScore += HandleOnSendScore;
    }

    private void Start()
    {
        Time.timeScale = 1.0f;
        points = 0;
        score.text = points.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        totalTime -= Time.deltaTime;
        if (totalTime <= 0)
        {
            gameInfo.SetActive(false);
            Time.timeScale = 0f;
            finalScore.text = points.ToString();
            endScreen.SetActive(true);
        }
        else
        {
            UpdateLevelTimer(totalTime);
        }
    }

    private void PowerUpNotification(string name)
    {
        powerup.text += name + " ACTIVE\n";
    }

    private void PowerDownNotification(string name)
    {
        powerup.text = powerup.text.Replace(name + " ACTIVE\n", "");
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

    void HandleOnSendScore(int num)
    {
        points += num;
        score.text = points.ToString();
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
