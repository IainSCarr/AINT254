using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour {

    private bool chance;

    private void Start()
    {
        if (gameObject.name == "ChancePowerUp")
        {
            chance = true;
        }

        iTween.ScaleFrom(gameObject, iTween.Hash("scale", Vector3.zero, "easetype", iTween.EaseType.easeOutBounce));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Object")
        {
            if (chance)
            {
                transform.SendMessageUpwards("DoRandomPowerUp");
                transform.SendMessageUpwards("RandomPowerUpDestroyed", transform.parent);
            }
            else
            {
                transform.SendMessageUpwards("DoGoodPowerUp");
                transform.SendMessageUpwards("GoodPowerUpDestroyed", transform.parent);
            }

            Destroy(gameObject);
        }
    }
}
