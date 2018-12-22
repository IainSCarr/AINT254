using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class PotBehaviour : MonoBehaviour {

    private void Awake()
    {
        //ActivatePowerUps.OnRotateTargets += HandleOnRotateTargets;
    }

    // Use this for initialization
    void Start() {
        iTween.MoveFrom(gameObject, iTween.Hash("y", 20));
    }

    private void HandleOnRotateTargets(bool rotating)
    {
        GetComponent<AutoMoveAndRotate>().enabled = rotating;
    }

    public void Hit()
    {
        Invoke("FlyAway", 1f);
        AudioManager.instance.PlaySound("TargetHit");
        SendMessageUpwards("ObjectDestroyed", transform.parent);
        SendMessage("DoSendScore");
    }

    private void FlyAway()
    {
        Invoke("Die", 2f);
        AudioManager.instance.PlaySound("TargetDeath");
        iTween.MoveTo(gameObject, iTween.Hash("y", 20, "time", 2f));
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        //ActivatePowerUps.OnRotateTargets -= HandleOnRotateTargets;
    }

    private void OnDestroy()
    {
        CancelInvoke();
    }
}
