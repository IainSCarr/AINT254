using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientManager : MonoBehaviour {

    public GameObject[] prefabArray = new GameObject[3];

    private string[] requiredIngredients;


    private void Awake()
    {
        requiredIngredients = new string[6];
        Instantiate(prefabArray[0], gameObject.transform);
    }
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void AddRecipe()
    {

    }
}
