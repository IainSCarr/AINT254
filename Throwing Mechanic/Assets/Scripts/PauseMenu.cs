using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public static bool GameisPaused = false;

    public GameObject PauseUI;

    private UIManager manager;

    AudioManager audioManager;

    void Start()
    {
        audioManager = AudioManager.instance;
        manager = GetComponent<UIManager>();

        GameisPaused = false;

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

        // if game has started
        if (manager.GetHasGameStarted())
        {
            // Play sounds
            audioManager.PlaySound("StartGame");
            audioManager.PlaySound("GameMusic");
        }
    }

    void Pause()
    {
        PauseUI.SetActive(true);
        Time.timeScale = 0f;
        GameisPaused = true;
        audioManager.StopSound("GameMusic");
    }
}
