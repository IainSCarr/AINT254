using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourChanger : MonoBehaviour {

    private Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void ChangeColour()
    {
        rend.material.color = Color.green;
    }
}
