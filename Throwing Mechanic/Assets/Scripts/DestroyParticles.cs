using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticles : MonoBehaviour {

    private bool isQuitting;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, GetComponent<ParticleSystem>().main.duration);
	}
}
