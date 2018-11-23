using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour {

    private bool chance;

    private void Start()
    {
        if (gameObject.tag == "ChancePowerUp")
        {
            chance = true;
        }

        iTween.ScaleFrom(gameObject, iTween.Hash("scale", Vector3.zero));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Object")
        {
            if (chance)
            {
                int coinFlip = Random.Range(0, 2);

                if (coinFlip == 0)
                {
                    transform.SendMessageUpwards("DoGoodPowerUp");
                }
                else
                {
                    transform.SendMessageUpwards("DoBadPowerUp");
                }
            }
            else
            {
                transform.SendMessageUpwards("DoGoodPowerUp");
            }

            transform.SendMessageUpwards("PowerUpDestroyed", transform.parent);
            Destroy(gameObject);
        }
    }
}
