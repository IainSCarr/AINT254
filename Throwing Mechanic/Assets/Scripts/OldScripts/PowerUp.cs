using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    private PowerUpManager manager;

    private void Start()
    {
        manager = FindObjectOfType<PowerUpManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Object")
        {
            manager.DoPowerUp(Random.Range(0, 5));
            StartCoroutine(RespawnTime());
            gameObject.GetComponent<Renderer>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }

    IEnumerator RespawnTime()
    {
        yield return new WaitForSeconds(15);

        gameObject.GetComponent<Renderer>().enabled = true;
        gameObject.GetComponent<Collider>().enabled = true;
    }

}
