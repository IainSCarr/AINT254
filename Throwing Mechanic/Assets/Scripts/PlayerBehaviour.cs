using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

    private int health = 60;

    private int speed;

    public delegate void UpdateHealth(int health);
    public static event UpdateHealth OnUpdateHealth;

    private GameObject currentObject;
    public ObjectManager objectManager;
    public SelectedObjectHighlight highlight;

    private int numObjects;
    private int selectedObject;

    // Use this for initialization
    void Start () {
        selectedObject = 0;
        numObjects = objectManager.GetNumObjects();
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
        // animate up
        iTween.MoveTo(gameObject, iTween.Hash("y", 1f, "time", 0.1f, "oncomplete", "Fall", "easetype", iTween.EaseType.easeOutQuad));
    }

    private void Fall()
    {
        // animate down
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

    public void DestroyObject()
    {
        Destroy(currentObject);
        SetCurrentObject(null);
    }

    public int GetSelectedObject()
    {
        return selectedObject;
    }

    /// <summary>
    /// Chooses next object and updates view
    /// </summary>
    public void NextObject()
    {
        if (selectedObject == numObjects - 1)
        {
            selectedObject = 0;
        }
        else
        {
            selectedObject++;
        }

        objectManager.SwitchObject();
        highlight.NextObject();
    }

    /// <summary>
    /// Chooses previous object and updates view
    /// </summary>
    public void PreviousObject()
    {
        if (selectedObject == 0)
        {
            selectedObject = numObjects - 1;
        }
        else
        {
            selectedObject--;
        }

        objectManager.SwitchObject();
        highlight.PreviousObject();
    }
}
