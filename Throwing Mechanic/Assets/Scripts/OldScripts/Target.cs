using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    private bool hasBeenHit;
    public Material[] materials;

    private Renderer rend;

    public delegate void SendScore(int score);
    public static event SendScore OnSendScore;

    private void OnEnable()
    {
        InputManager.OnSpaceBarPress += HandleOnSpaceBarPress;
    }

    private void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material = materials[Random.Range(0, 3)];
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

                if (OnSendScore != null)
                {
                    OnSendScore(5);
                }
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
