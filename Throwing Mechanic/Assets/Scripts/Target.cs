using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    private bool hasBeenHit;
    public Material[] materials;

    private Renderer rend;

    private void OnEnable()
    {
        InputManager.OnSpaceBarPress += HandleOnSpaceBarPress;
    }

    private void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material = materials[Random.Range(0, 3)];
        Debug.Log(rend.material.name);
        Debug.Log(rend.sharedMaterial.name.Replace("(Instance)", ""));
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Object" && rend.sharedMaterial.name.Replace(" (Instance)", "") == collision.gameObject.name.Replace("(Clone)", ""))
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
        if (hasBeenHit)
        {
            rend.material = materials[Random.Range(0, 3)];
        }
        hasBeenHit = false;
    }
}
