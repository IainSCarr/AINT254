using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotCollider : MonoBehaviour {

    private bool hasBeenHit;

    private PotBehaviour pot;

    public delegate void SendScore(int score);
    public static event SendScore OnSendScore;

    private void Start()
    {
        pot = GetComponentInParent<PotBehaviour>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Remove(other.gameObject.name.Length - 7) == pot.GetTarget())
        {
            if (!hasBeenHit)
            {
                hasBeenHit = true;

                if (OnSendScore != null)
                {
                    OnSendScore(15);
                }

                SendMessageUpwards("Hit");
            }
        }
    }
}
