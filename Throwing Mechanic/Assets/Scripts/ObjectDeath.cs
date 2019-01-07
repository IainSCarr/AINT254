using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDeath : MonoBehaviour {

    public float deathTime = 0.2f;
    public float scaleAmount = 0.2f;

    private bool hasHitObject = false;

    void OnCollisionEnter(Collision collision)
    {
        // if collides with enemy
        if (collision.gameObject.tag == "Enemy")
        {
            StartDeath();
            hasHitObject = true;
        }
        else if (collision.gameObject.tag == "Obstacle")
        {
            AudioManager.instance.PlaySound("HitBrick");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Target")
        {
            hasHitObject = true;

            // if can explode
            if (GetComponent<MultiplyExplode>().GetCanExplode())
            {
                StartDeath();
            }
            else
            {
                // destroy immediately
                Die();
            }
        }
        else if (other.gameObject.tag == "PowerUp")
        {
            hasHitObject = true;
        }
    }


    /// <summary>
    /// Animates object and calls death function upon completion.
    /// </summary>
    public void StartDeath()
    {
        if (GetComponent<MultiplyExplode>().GetCanExplode())
        {
            Invoke("Die", deathTime);
            // Animate death
            //iTween.ScaleBy(gameObject, iTween.Hash("x", scaleAmount, "y", scaleAmount, "z", scaleAmount, "time", deathTime));
            //iTween.ColorTo(gameObject, iTween.Hash("color", Color.red, "time", deathTime));
        }
    }

    /// <summary>
    /// Calls explode function then destroys object
    /// </summary>
    public void Die()
    {
        SendMessage("Explode", SendMessageOptions.DontRequireReceiver);

        if (!hasHitObject)
        {
            SendMessageUpwards("NoObjectHit");
        }

        Destroy(gameObject);
    }
}
