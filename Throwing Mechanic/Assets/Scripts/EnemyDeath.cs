using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour {

    public ParticleSystem particles;

    public float deathTime = 0.2f;
    public float shakeAmount = 0.2f;

    private bool isQuitting;

	// Use this for initialization
	void Start () {
        Invoke("StartDeath", 20);
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
