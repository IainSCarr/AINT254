using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectModel : MasterElement {

    public Transform objectTransform;
    public Rigidbody objectRigidbody;

    public string ingredientName;

    // Use this for initialization
    void Start () {
        objectTransform = gameObject.GetComponent<Transform>();
        objectRigidbody = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	public void UpdateObject() {
		objectTransform = gameObject.GetComponent<Transform>();
        objectRigidbody = gameObject.GetComponent<Rigidbody>();
    }
}
