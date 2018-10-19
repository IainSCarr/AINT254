using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    private int[] powerups;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Object")
        {
            Debug.Log("PowerUp");
            Physics.gravity = new Vector3(0,-10,0);
            Destroy(gameObject);
        }
    }
    
    private void DoPowerUp()
    {

    }

}
