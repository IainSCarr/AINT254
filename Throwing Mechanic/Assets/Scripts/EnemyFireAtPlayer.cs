using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireAtPlayer : MonoBehaviour
{
    public GameObject bullet;
    public Transform origin;

    private float normalFireRate = 1.0f;
    private float fireRate;

    private bool isShootingFast;

    private void OnEnable()
    {
        ActivatePowerUps.OnIncreaseFireRate += HandleOnIncreaseFireRate;
    }

    // Use this for initialization
    void Start()
    {
        fireRate = normalFireRate;

        iTween.MoveTo(gameObject, iTween.Hash("y", 0.5, "time", 0.5f, "easetype", iTween.EaseType.spring));
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
        Instantiate(bullet, origin.position, transform.rotation);
    }

    void OnDestroy()
    {
        CancelInvoke("Shoot");
        ActivatePowerUps.OnIncreaseFireRate -= HandleOnIncreaseFireRate;
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
