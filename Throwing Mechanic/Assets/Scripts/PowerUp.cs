using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    public GameObject UI;

    private int[] powerups;
    private string[] powerupNames = new string[5] { "Increased Gravity", "Decreased gravity", "Quickfire", "Triple-Shot", "Explode" };

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Object")
        {
            DoPowerUp(Random.Range(0, 5));
            Destroy(gameObject);
        }
    }
    
    private void DoPowerUp(int num)
    {
        if (num == 0)
        {
            Physics.gravity = new Vector3(0, -30, 0);
        }
        else if (num == 1)
        {
            Physics.gravity = new Vector3(0, -10, 0);
        }
        else if (num == 2)
        {

        }
        else if (num == 3)
        {

        }
        else if (num == 4)
        {

        }

        PowerUpNotify(num);
    }

    private void PowerUpNotify(int num)
    {
        UI.SendMessage("PowerUpNotification", powerupNames[num]);
    }
}
