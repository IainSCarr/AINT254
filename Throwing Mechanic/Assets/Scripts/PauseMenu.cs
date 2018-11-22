using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public static bool GameisPaused = false;

    public GameObject PauseUI;

    AudioManager audioManager;

    void Start()
    {
        audioManager = AudioManager.instance;

        if (audioManager == null)
        {
            Debug.LogError("No audio manager found in scene.");
        }
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameisPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
	}

    void Resume()
    {
        PauseUI.SetActive(false);
        Time.timeScale = 1f;
        GameisPaused = false;
        audioManager.PlaySound("StartGame");
        audioManager.PlaySound("GameplayMusic");
    }

    void Pause()
    {
        PauseUI.SetActive(true);
        Time.timeScale = 0f;
        GameisPaused = true;
        audioManager.StopSound("GameMusic");
    }
}
