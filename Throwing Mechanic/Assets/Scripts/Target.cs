using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    private bool hasBeenHit;

    private Renderer rend;

    private void OnEnable()
    {
        InputManager.OnSpaceBarPress += HandleOnSpaceBarPress;
    }

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Object")
        {
            if (!hasBeenHit)
            {
                collision.gameObject.SendMessage("Explode");
                hasBeenHit = true;
                ChangeColour();
            }
        }
    }

    private void ChangeColour()
    {
        rend.material.color = Color.green;
    }

    void HandleOnSpaceBarPress()
    {
        hasBeenHit = false;
        rend.material.color = Color.yellow;
    }
}
