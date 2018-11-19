using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class PotBehaviour : MonoBehaviour {

    private void Awake()
    {
        ActivatePowerUps.OnRotateTargets += HandleOnRotateTargets;
    }

    // Use this for initialization
    void Start() {
        iTween.MoveFrom(gameObject, iTween.Hash("y", 20));
    }

    private void HandleOnRotateTargets(bool rotating)
    {
        GetComponent<AutoMoveAndRotate>().enabled = rotating;
    }
}
