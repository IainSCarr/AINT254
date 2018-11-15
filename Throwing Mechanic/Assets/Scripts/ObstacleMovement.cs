using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour {

    public float slideTime = 5f;

    private float startTime;

    private bool isQuitting;

    // Use this for initialization
    void Start () {
        iTween.MoveFrom(gameObject, iTween.Hash("y", -5, "easetype", iTween.EaseType.easeInOutBounce, "oncomplete", "StartMoving"));

        startTime = Time.time;
    }

    void Update()
    {
        if (Time.time - startTime >= 30)
        {
            iTween.MoveTo(gameObject, iTween.Hash("y", -5, "easetype", iTween.EaseType.easeInOutBounce, "oncomplete", "Die"));
        }
    }

    private void StartMoving()
    {
        iTween.MoveTo(gameObject, iTween.Hash("x", -20, "time", slideTime, "looptype", "pingpong", "easetype", iTween.EaseType.easeInOutQuad));
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnApplicationQuit()
    {
        isQuitting = true;
    }

    private void OnDestroy()
    {
        if (!isQuitting)
        {
            transform.SendMessageUpwards("ObstacleDestroyed", transform.parent);
        }
    }
}
