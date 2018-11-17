using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotCollider : MonoBehaviour {

    private bool hasBeenHit;

    private Renderer rend;

    public delegate void SendScore(int score);
    public static event SendScore OnSendScore;

    private void OnEnable()
    {
        InputManager.OnSpaceBarPress += HandleOnSpaceBarPress;

    }

    private void Start()
    {
        rend = GetComponentInParent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasBeenHit)
        {
            other.gameObject.SendMessage("Explode");
            hasBeenHit = true;
            ChangeColour();

            if (OnSendScore != null)
            {
                OnSendScore(15);
            }
        }
    }

    void HandleOnSpaceBarPress()
    {
        hasBeenHit = false;
        rend.material.color = Color.yellow;
    }


    private void ChangeColour()
    {
        rend.material.color = Color.green;
    }
}
