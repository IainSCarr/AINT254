using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    private Transform playerTransform;
    private Transform cameraFollowTransform;

    public float characterSpeed = 5.0f;

    private float dampner = 0.05f;
    private float realCharacterSpeed;
	
    // Use this for initialization
	void Start () {
        playerTransform = GetComponent<Transform>();
        cameraFollowTransform = transform.GetChild(0);

        realCharacterSpeed = characterSpeed * dampner;
    }
	
	// Update is called once per frame
	void Update () {
	}

    void FixedUpdate()
    {
        MovePlayer(Input.GetAxis("Horizontal"));
        // Clamp player to play area
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -31.5f, 31.5f), playerTransform.position.y, playerTransform.position.z);

        MoveCamera(Input.GetAxis("Vertical"));
        // Clamp camera position
        cameraFollowTransform.position = new Vector3(playerTransform.position.x, Mathf.Clamp(cameraFollowTransform.position.y, 3f, 7.5f), playerTransform.position.z);
    }

    /// <summary>
    /// Moves the player left or right based on input
    /// </summary>
    private void MovePlayer(float amount)
    {
        playerTransform.position += new Vector3(amount, 0) * realCharacterSpeed;
    }

    private void MoveCamera(float amount)
    {
        cameraFollowTransform.position += new Vector3(0, amount) * dampner;
    }

    public void SetSpeed(float speed)
    {
        realCharacterSpeed = speed;
    }
}
