using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour {

    public void Spawn(GameObject newObject)
    {
        Instantiate(newObject, transform.position, transform.rotation, transform);
    }
}
