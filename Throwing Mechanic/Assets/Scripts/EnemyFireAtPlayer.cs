using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireAtPlayer : MonoBehaviour
{
    public GameObject bullet;
    public Transform origin;

    private float normalFireRate = 0.7f;
    private float fireRate;

    private bool isShootingFast;

    private void OnEnable()
    {
        BarragePowerUp.OnIncreaseFireRate += HandleOnIncreaseFireRate;
    }

    // Use this for initialization
    void Start()
    {
        fireRate = normalFireRate;

        AudioManager.instance.PlaySound("EnemySpawn");

        iTween.MoveTo(gameObject, iTween.Hash("y", -1, "time", 0.5f, "easetype", iTween.EaseType.spring));
        iTween.ScaleFrom(gameObject, iTween.Hash("x", 0, "y", 0, "z", 0, "time", 0.5f, "oncomplete", "StartShooting"));
    }

    // Update is called once per frame
    void Update()
    {
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
        AudioManager.instance.PlaySound("EnemyShoot");
        Instantiate(bullet, origin.position, transform.rotation);
    }

    void OnDestroy()
    {
        CancelInvoke("Shoot");
        BarragePowerUp.OnIncreaseFireRate -= HandleOnIncreaseFireRate;
    }

    private void HandleOnIncreaseFireRate(float rate)
    {
        if (!isShootingFast)
        {
            isShootingFast = true;
            ChangeFireRate(rate);
        }
    }

    IEnumerator ResetFireRate()
    {
        yield return new WaitForSeconds(10.0f);

        isShootingFast = false;
        ChangeFireRate(normalFireRate);
    }

    private void ChangeFireRate(float rate)
    {
        StopShooting();
        fireRate = rate;
        StartShooting();

        if (isShootingFast)
        {
            StartCoroutine(ResetFireRate());
        }
    }
}
