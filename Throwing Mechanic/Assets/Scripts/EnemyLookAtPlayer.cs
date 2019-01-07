using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLookAtPlayer : MonoBehaviour {

    private GameObject player;
    private Transform playerPosition;

	// Use this for initialization
	void Start () {
        // finds player
        player = GameObject.FindGameObjectWithTag("Player");
        // gets position
        playerPosition = player.GetComponent<Transform>();
	}

    void LateUpdate()
    {
        Vector3 targetPostition = new Vector3(playerPosition.position.x,
                                       this.transform.position.y,
                                       playerPosition.position.z);
        transform.LookAt(targetPostition);

        //transform.LookAt(playerPosition);
    }
}
