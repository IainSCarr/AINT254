using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterElement : MonoBehaviour {

    // Gives access to the application and all instances.
    public MasterApplication app { get { return GameObject.FindObjectOfType<MasterApplication>(); } }
}
