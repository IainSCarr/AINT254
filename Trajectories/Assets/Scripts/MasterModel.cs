using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Contains all data related to the app.
public class MasterModel : MasterElement {

    public PlayerModel player;
    public ObjectModel currentObject;

    public GameObject[] prefabs;

    private int selector = 0;

    void Start() {
        
    }

    public void UpdateObject()
    {
        if (selector == 2)
        {
            selector = 0;
        }
        else
        {
            selector++;
        }

        currentObject = prefabs[selector].GetComponent<ObjectModel>();
    }
}
