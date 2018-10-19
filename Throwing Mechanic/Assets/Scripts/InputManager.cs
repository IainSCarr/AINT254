using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public GameObject player;
    public GameObject projectile;

    public delegate void SpaceBarPress();
    public static event SpaceBarPress OnSpaceBarPress;

    private Vector3 startingPoint;

    private float smoothing = 0.1f;

	// Use this for initialization
	void Start () {
        startingPoint = projectile.GetComponent<Transform>().position;
	}
	
	// Update is called once per frame
	void Update () {
        MovePlayer(Input.GetAxis("Horizontal") * smoothing);

        if (true)
        {

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetObjectPosition();
            player.SendMessage("Reset");

            if (OnSpaceBarPress != null)
            {
                OnSpaceBarPress();
            }
        }
	}

    private void MovePlayer(float num)
    {
        player.transform.Translate(num, 0, 0);
    }

    private void ResetObjectPosition()
    {
        projectile.transform.position = startingPoint;
        projectile.GetComponent<Rigidbody>().velocity = Vector3.zero;
        projectile.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        projectile.GetComponent<Transform>().rotation = Quaternion.identity;
    }
}
