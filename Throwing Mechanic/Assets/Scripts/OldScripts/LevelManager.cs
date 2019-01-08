using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    private AudioManager instance;

    // Use this for initialization
    void Start () {
        instance = AudioManager.instance;
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
        {
            instance.PlaySound("TitleMusic");
        }
    }

    public void LoadGame()
    {
        instance.StopSound("TitleMusic");
        instance.PlaySound("ClickButton");
        SceneManager.LoadScene("Game");
    }

    public void LoadMenu()
    {
        instance.StopSound("GameMusic");
        instance.PlaySound("TitleMusic");
        instance.PlaySound("ClickButton");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScreen");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
