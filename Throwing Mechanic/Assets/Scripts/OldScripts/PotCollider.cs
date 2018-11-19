using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotCollider : MonoBehaviour {

    private bool hasBeenHit;

    private Renderer rend;

    public delegate void SendScore(int score);
    public static event SendScore OnSendScore;


    private void Start()
    {
        rend = GetComponentInParent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasBeenHit)
        {
            hasBeenHit = true;
            ChangeColour();

            other.gameObject.SendMessage("Explode");

            if (OnSendScore != null)
            {
                OnSendScore(15);
            }
        }
    }

    private void ChangeColour()
    {
        rend.material.color = Color.green;
    }
}
