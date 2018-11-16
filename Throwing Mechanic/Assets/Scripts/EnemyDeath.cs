using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour {

    public ParticleSystem particles;

    public float deathTime = 0.2f;
    public float shakeAmount = 0.2f;

    private bool isQuitting;

    // Use this for initialization

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Object")
        {
            StartDeath();
        }
    }

    public void StartDeath()
    {
        Invoke("Die", deathTime);
        // Animate death
        iTween.ShakePosition(gameObject, iTween.Hash("x", shakeAmount, "z", shakeAmount, "time", deathTime));
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnApplicationQuit()
    {
        // prevents errors if application is closed during enemy death
        isQuitting = true;
    }

    private void OnDestroy()
    {
        if (!isQuitting)
        {
            Instantiate(particles, transform.position, transform.rotation);

            // Send message to enemy manager with parent reference
            transform.SendMessageUpwards("EnemyDestroyed", transform.parent);
        }
    }
}
