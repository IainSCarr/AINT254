using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedObjectHighlight : MonoBehaviour {

    private int selectedObject;

    private Color hidden = new Color(1, 1, 1, 0.3f);
    private Color selected = new Color(1, 1, 1, 1f);

    public Image[] images;

	// Use this for initialization
	void Start () {
        selectedObject = 0;

        for (int i = 0; i < images.Length; i++)
        {
            HideObject(i);
        }

        HighlightObject(selectedObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void HighlightObject(int num)
    {
        images[num].color = selected;
    }

    private void HideObject(int num)
    {
        images[num].color = hidden;
    }

    public void NextObject()
    {
        HideObject(selectedObject);
        if (selectedObject == images.Length - 1)
        {
            selectedObject = 0;
        }
        else
        {
            selectedObject++;
        }
        HighlightObject(selectedObject);
    }

    public void PreviousObject()
    {
        HideObject(selectedObject);
        if (selectedObject == 0)
        {
            selectedObject = images.Length - 1;
        }
        else
        {
            selectedObject--;
        }
        HighlightObject(selectedObject);
    }

    public Image[] GetImages()
    {
        return images;
    }
}
