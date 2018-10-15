using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourChanger : MonoBehaviour {

    private Renderer rend;
    private Collider targetCollider;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        targetCollider = GetComponent<BoxCollider>();
    }

    void ChangeColour()
    {
        rend.material.color = Color.green;
    }

    void DisableMultiplication()
    {
        targetCollider.isTrigger = true;
    }
}
