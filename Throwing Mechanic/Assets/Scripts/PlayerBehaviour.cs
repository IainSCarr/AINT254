using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

    private int health = 60;

    private int speed;

    public delegate void UpdateHealth(int health);
    public static event UpdateHealth OnUpdateHealth;

    private GameObject currentObject;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (OnUpdateHealth != null)
        {
            OnUpdateHealth(health);
        }
    }

    public void Jump()
    {
        iTween.MoveTo(gameObject, iTween.Hash("y", 1f, "time", 0.1f, "oncomplete", "Fall", "easetype", iTween.EaseType.easeOutQuad));
    }

    private void Fall()
    {
        iTween.MoveTo(gameObject, iTween.Hash("y", 0.5f, "time", 0.1f, "easetype", iTween.EaseType.easeInQuad));
    }

    public void SetCurrentObject(GameObject obj)
    {
        currentObject = obj;
    }

    public GameObject GetCurrentObject()
    {
        return currentObject;
    }
}
