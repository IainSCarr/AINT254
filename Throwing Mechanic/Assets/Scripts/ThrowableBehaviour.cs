using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableBehaviour : MonoBehaviour {

    private GameObject player;
    private Transform playerPos;

    private MultiplyExplode exploder;

    private bool isCurrentObject = true;

    // Use this for initialization
    void Start() {
        // find player
        player = GameObject.FindGameObjectWithTag("Player");

        // set itself to players current object
        player.GetComponent<PlayerBehaviour>().SetCurrentObject(gameObject);

        playerPos = player.GetComponent<Transform>();

        iTween.ScaleFrom(gameObject, iTween.Hash("x", 0, "y", 0, "z", 0));
    }

    void FixedUpdate() {
        if (isCurrentObject)
        {
            // lock position to directly behind player
            transform.position = playerPos.position - (playerPos.forward * 2);
        }
    }

    public void Throw(Vector3 direction, float magnitude)
    {
        Invoke("Die", 10f);
        // disconnect object from player
        isCurrentObject = false;
        player.GetComponent<PlayerBehaviour>().SetCurrentObject(null);
        gameObject.SendMessageUpwards("StartSpawn");

        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().AddForce(direction * magnitude, ForceMode.Impulse);
        GetComponent<Rigidbody>().AddTorque(new Vector3(1, 0, -direction.x) * magnitude);
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        CancelInvoke("Die");
    }
}
