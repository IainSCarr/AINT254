using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour {

    public float slideTime = 5f;

    private float startTime;
    private bool hasDied;

    // Use this for initialization
    void Start () {
        AudioManager.instance.PlaySound("ObstacleSpawn");
        iTween.MoveFrom(gameObject, iTween.Hash("y", -5, "easetype", iTween.EaseType.easeOutQuad, "oncomplete", "StartMoving", "time", 1.5f));

        startTime = Time.time;
    }

    void Update()
    {
        if (Time.time - startTime >= 30)
        {
            if (!hasDied)
            {
                hasDied = true;
                AudioManager.instance.PlaySound("ObstacleSpawn");
                iTween.MoveTo(gameObject, iTween.Hash("y", -5, "easetype", iTween.EaseType.easeInOutQuad, "oncomplete", "Die", "time", 2f));
            }

        }
    }

    private void StartMoving()
    {
        iTween.MoveTo(gameObject, iTween.Hash("x", -20, "time", slideTime, "looptype", "pingpong", "easetype", iTween.EaseType.easeInOutQuad));
    }

    private void Die()
    {
        transform.SendMessageUpwards("ObstacleDestroyed", transform.parent);
        Destroy(gameObject);
    }
}
