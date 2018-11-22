using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{

    public AudioMixer audioMixer;
    AudioManager audioManager;

    void Start()
    {
        audioManager = AudioManager.instance;

        if (audioManager == null)
        {
            Debug.LogError("No audio manager found in scene.");
        }
    }

    public void ChangeMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }

    public void ChangeSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
        audioManager.PlaySound("EnemySpawn");
    }
}
