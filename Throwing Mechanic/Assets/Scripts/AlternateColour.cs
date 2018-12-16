using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternateColour : MonoBehaviour {

    public Color startColour;
    public Color endColour;

    public float speed;

	// Use this for initialization
	void Start () {
        GetComponent<Renderer>().material.color = startColour;

        //iTween.ColorTo(gameObject, iTween.Hash("color", endColour, "time", speed, "looptype", iTween.LoopType.pingPong));
	}
}
