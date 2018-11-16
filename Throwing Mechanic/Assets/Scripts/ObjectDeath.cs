using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDeath : MonoBehaviour {

    public float deathTime = 0.2f;
    public float scaleAmount = 0.2f;

    private bool isQuitting;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // if the object can explode
            if (GetComponent<MultiplyExplode>().GetCanExplode())
            {
                StartDeath();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// Animates object and calls death function upon completion.
    /// </summary>
    public void StartDeath()
    {
        Invoke("Die", deathTime);

        // Animate death
        iTween.ScaleBy(gameObject, iTween.Hash("x", scaleAmount, "y", scaleAmount, "z", scaleAmount, "time", deathTime));
        iTween.ColorTo(gameObject, iTween.Hash("color", Color.red, "time", deathTime));
    }

    /// <summary>
    /// Calls explode function then destroys object
    /// </summary>
    private void Die()
    {
        SendMessage("Explode", SendMessageOptions.DontRequireReceiver);
        Destroy(gameObject);
    }
}
