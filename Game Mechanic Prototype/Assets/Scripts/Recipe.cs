using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour {

    private string recipeName;
    private Ingredient[] ingredients;

    public Recipe(string recipe, Ingredient[] ingreds)
    {
        recipeName = recipe;
        ingredients = ingreds;
    }
}
