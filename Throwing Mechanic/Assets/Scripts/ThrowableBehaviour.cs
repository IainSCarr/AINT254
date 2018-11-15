using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableBehaviour : MonoBehaviour {

    private GameObject player;
    private Transform playerPos;

    private bool isCurrentObject = true;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerBehaviour>().SetCurrentObject(gameObject);

        playerPos = player.GetComponent<Transform>();
	}
	
	void FixedUpdate () {
        if (isCurrentObject)
        {
            transform.position = playerPos.position - (playerPos.forward * 2);
        }
    }

    public void Throw(Vector3 direction, float magnitude)
    {
        // disconnect object from player
        isCurrentObject = false;

        GetComponent<Rigidbody>().AddForce(direction * magnitude, ForceMode.Impulse);
        GetComponent<Rigidbody>().AddTorque(new Vector3(1, 0, -direction.x) * magnitude);
    }
}
