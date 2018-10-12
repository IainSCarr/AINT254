using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour {

    private new string name;
    private bool canMultiply;
    private int score;

    private void Start()
    {
        name = "Cube";
        canMultiply = true;
        score = 5;
    }
}
