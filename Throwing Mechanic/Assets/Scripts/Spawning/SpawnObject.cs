using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour {

    public GameObject Spawn(GameObject newObject)
    {
        GameObject createdObject = Instantiate(newObject, transform.position, transform.rotation, transform);
        return createdObject;
    }
}
