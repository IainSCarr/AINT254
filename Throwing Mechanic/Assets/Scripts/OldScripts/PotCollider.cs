using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotCollider : MonoBehaviour {

    private bool hasBeenHit;

    public delegate void SendScore(int score);
    public static event SendScore OnSendScore;

    private void OnTriggerEnter(Collider other)
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
