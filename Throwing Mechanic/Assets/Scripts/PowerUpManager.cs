using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour {

    public GameObject UI;

    private int[] powerups;
    private bool[] currentPowerUps = new bool[5];
    private string[] powerupNames = new string[5] { "Increased Gravity", "Decreased gravity", "Quickfire", "Triple-Shot", "Explode" };

    private ShowTrajectory player;

    // Use this for initialization
    void Start () {
        player = FindObjectOfType<ShowTrajectory>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DoPowerUp(int num)
    {
        if (!currentPowerUps[num])
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
            currentPowerUps[num] = true;
            PowerUpNotify(num);
            StartCoroutine(EndPowerUp(num));
        }
        else
        {
            DoPowerUp(Random.Range(0, 5));
        }

    }

    private void PowerUpNotify(int num)
    {
        UI.SendMessage("PowerUpNotification", powerupNames[num]);
    }

    private void PowerDownNotify(int num)
    {
        UI.SendMessage("PowerDownNotification", powerupNames[num]);
    }

    IEnumerator EndPowerUp(int num)
    {
        yield return new WaitForSeconds(15);
        currentPowerUps[num] = false;
        PowerDownNotify(num);
        if (num == 0 || num == 1)
        {
            Physics.gravity = new Vector3(0, -20, 0);
            Debug.Log("Gravity desctivatvated");
        }
        else if (num == 2)
        {
            player.SetFireRate(3f);
        }
        else if (num == 3)
        {

        }
        else if (num == 4)
        {

        }
    }
}
