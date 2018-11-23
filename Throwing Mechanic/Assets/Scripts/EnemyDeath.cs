using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour {

    public ParticleSystem particles;

    public float deathTime = 0.2f;
    public float shakeAmount = 0.2f;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Object")
        {
            StartDeath();
        }
    }

    public void StartDeath()
    {
        AudioManager.instance.PlaySound("EnemySqueel");
        Invoke("Die", deathTime);
        // Animate death
        iTween.ShakePosition(gameObject, iTween.Hash("x", shakeAmount, "z", shakeAmount, "time", deathTime));
    }

    private void Die()
    {
        AudioManager.instance.PlaySound("EnemyDeath");
        SendMessage("DoSendScore");
        transform.SendMessageUpwards("EnemyDestroyed", transform.parent);
        Instantiate(particles, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
