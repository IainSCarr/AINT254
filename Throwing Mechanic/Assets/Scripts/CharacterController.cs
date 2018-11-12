using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    private Transform playerTransform;

    public float speed = 5.0f;

    private float dampner = 0.05f;
    private float realSpeed;
	
    // Use this for initialization
	void Start () {
        playerTransform = GetComponent<Transform>();

        realSpeed = speed * dampner;

    }
	
	// Update is called once per frame
	void Update () {
	}

    void FixedUpdate()
    {
        MovePlayer(Input.GetAxis("Horizontal"));
        // Clamp player to play area
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -31.5f, 31.5f), playerTransform.position.y, playerTransform.position.z);

    }

    /// <summary>
    /// Moves the player left or right based on input
    /// </summary>
    private void MovePlayer(float amount)
    {
        playerTransform.position += new Vector3(amount, 0) * realSpeed;
    }

    public void SetSpeed(float speed)
    {
        realSpeed = speed;
    }
}
