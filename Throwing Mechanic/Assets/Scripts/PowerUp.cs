using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    public GameObject UI;

    private int[] powerups;
    private string[] powerupNames = new string[5] { "Increased Gravity", "Decreased gravity", "Quickfire", "Triple-Shot", "Explode" };

    private ShowTrajectory player;

    private void Start()
    {
        player = FindObjectOfType<ShowTrajectory>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Object")
        {
            DoPowerUp(Random.Range(0, 5));
            StartCoroutine(RespawnTime());
            gameObject.GetComponent<Renderer>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
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
            player.SetFireRate(0.5f);
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

    IEnumerator RespawnTime()
    {
        yield return new WaitForSeconds(10);

        gameObject.GetComponent<Renderer>().enabled = true;
        gameObject.GetComponent<Collider>().enabled = true;
    }
}
