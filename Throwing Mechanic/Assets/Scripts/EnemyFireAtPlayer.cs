using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState { Waiting, Firing, Barrage, Moving}

public class EnemyFireAtPlayer : MonoBehaviour
{
    public GameObject bullet;
    public Transform origin;

    private EnemyState state;
    private float normalFireRate = 0.7f;
    private float fireRate;

    private bool isShootingFast;

    private AudioManager instance;

    private void OnEnable()
    {
        BarragePowerUp.OnIncreaseFireRate += HandleOnIncreaseFireRate;
    }

    // Use this for initialization
    void Start()
    {
        state = EnemyState.Waiting;
        fireRate = normalFireRate;

        instance = AudioManager.instance;

        // animate spawn
        iTween.MoveTo(gameObject, iTween.Hash("y", -1, "time", 0.5f, "easetype", iTween.EaseType.spring));
        // calls start shooting on animate complete
        iTween.ScaleFrom(gameObject, iTween.Hash("x", 0, "y", 0, "z", 0, "time", 0.5f, "oncomplete", "StartShooting"));
    }

    public void StartShooting()
    {
        if (isShootingFast)
        {
            InvokeRepeating("Shoot", 5f, fireRate);
        }
        else
        {
            InvokeRepeating("Shoot", 0f, fireRate);
        }
    }

    public void StopShooting()
    {
        CancelInvoke("Shoot");
    }

    private void Shoot()
    {
        instance.PlaySound("EnemyShoot");
        //Instantiate(bullet, origin.position, transform.rotation);
        SpawnBullet();
    }

    void OnDestroy()
    {
        CancelInvoke("Shoot");
        BarragePowerUp.OnIncreaseFireRate -= HandleOnIncreaseFireRate;
    }

    /// <summary>
    /// Handler for barrage powerup
    /// </summary>
    /// <param name="rate"></param>
    private void HandleOnIncreaseFireRate(float rate)
    {
        if (!isShootingFast)
        {
            isShootingFast = true;
            state = EnemyState.Barrage;
            ChangeFireRate(rate);
        }
    }

    IEnumerator ResetFireRate()
    {
        yield return new WaitForSeconds(10.0f);

        isShootingFast = false;
        state = EnemyState.Firing;
        ChangeFireRate(normalFireRate);
    }

    private void ChangeFireRate(float rate)
    {
        StopShooting();
        fireRate = rate;
        StartShooting();

        // if barrage
        if (isShootingFast)
        {
            // start reset
            StartCoroutine(ResetFireRate());
        }
    }

    public EnemyState GetState()
    {
        return state;
    }

    private void SpawnBullet()
    {
        GameObject bullet = PoolManager.current.GetPooledObject("Knife");

        if (bullet != null)
        {
            bullet.transform.position = origin.position;
            bullet.transform.rotation = origin.rotation;
            bullet.SetActive(true);
        }
    }
}
