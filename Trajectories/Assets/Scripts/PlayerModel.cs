using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MasterElement {

    private Transform playerPosition;

	void Start () {
        playerPosition = GetComponent<Transform>();
	}

    public void MovePlayer(float input)
    {
        playerPosition.Translate(input, 0, 0);
    }
}
