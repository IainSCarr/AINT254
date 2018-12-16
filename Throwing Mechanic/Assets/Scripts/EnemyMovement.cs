using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    private Vector3 scale = new Vector3(0.05f, 0.05f, 0.05f);
    private EnemyFireAtPlayer fireScript;
    private bool direction;

    private float shootTime = 9f;
    private float hideTime = 3f;

	// Use this for initialization
	void Start () {
        fireScript = GetComponent<EnemyFireAtPlayer>();
        Invoke("StartMoving", shootTime);
    }

    private void StartMoving()
    {
        if (fireScript.GetState() == EnemyState.Barrage)
        {
            Invoke("StartMoving", 10f);
        }
        else
        {
            SendMessage("StopShooting");

            iTween.ScaleTo(gameObject, iTween.Hash("scale", Vector3.zero, "time", 0.2f, "easetype", iTween.EaseType.easeInBack, "oncomplete", "Move"));
        }
    }

    private void Move()
    {
        if (direction)
        {
            transform.position += Vector3.right * 5;
            direction = false;
        }
        else
        {
            transform.position += Vector3.left * 5;
            direction = true;
        }

        StartCoroutine(Hide());
    }

    private void StopMoving()
    {
        SendMessage("StartShooting");

        Invoke("StartMoving", shootTime);
    }

    IEnumerator Hide()
    {
        yield return new WaitForSeconds(hideTime);

        iTween.ScaleTo(gameObject, iTween.Hash("scale", scale, "time", 0.2f, "easetype", iTween.EaseType.easeOutBack, "oncomplete", "StopMoving"));

        yield break;
    }

    public void SetFastEnemy()
    {
        shootTime /= 3;
        hideTime /= 3;
    }
}
