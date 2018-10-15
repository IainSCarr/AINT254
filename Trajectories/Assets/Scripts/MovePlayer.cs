using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {

    private Transform playerTransform;
    private float smoothing = 0.05f;

    private void Start()
    {
        playerTransform = transform;
    }

    void Update () {
        float moveHoriz = Input.GetAxis("Horizontal") * smoothing;
        Vector3 movement = new Vector3(moveHoriz, 0.0f, 0.0f);
        playerTransform.position += movement;
    }
}
