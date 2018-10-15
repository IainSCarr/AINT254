using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryView : MasterView {

    public GameObject dotPrefab;
    [SerializeField]
    private GameObject[] dots;

    private Vector3 objectVector;
    private Vector3 aimVector;

    private float magnitude;

    void Start()
    {
        dots = new GameObject[10];

        for (int i = 0; i < 10; i++)
        {
            GameObject tempDot = Instantiate(dotPrefab);

            tempDot.SetActive(false);

            dots[i] = tempDot;
        }
    }

    public void DisplayTrajectory(Vector3 mousePosition)
    {
        objectVector = cam.WorldToScreenPoint(app.model.currentObject.objectTransform.position);
        objectVector.z = 0;

        aimVector = (objectVector - mousePosition).normalized;
        aimVector.z = aimVector.y;
        magnitude = (objectVector - mousePosition).magnitude * 0.1f;

        Aim(aimVector, magnitude);
    }

    public void HideTrajectory()
    {
        for (int i = 0; i < 10; i++)
        {
            dots[i].SetActive(false);
        }
    }

    private void Aim(Vector3 direction, float magnitude)
    {
        float Sx = direction.x * magnitude;
        float Sy = direction.y * magnitude;

        for (int i = 0; i < 10; i++)
        {
            float time = i * 0.2f;

            float dX = Sx * time;
            float dY = (Sy * time) - (0.5f * (Physics.gravity.y * -1) * time * time);
            float dZ = Sy * time;

            Vector3 dotPosition = new Vector3(dX, dY, dZ);

            dots[i].SetActive(true);

            if (i != 0)
            {
                Color newColour = dots[i].GetComponent<Renderer>().material.color;
                newColour.a = 1f / time * 0.4f;
                dots[i].GetComponent<Renderer>().material.color = newColour;
            }

            dots[i].transform.position = app.model.currentObject.objectTransform.position + dotPosition;
        }
    }

    public void ThrowObject()
    {
        app.model.currentObject.objectRigidbody.AddForce(aimVector * magnitude, ForceMode.Impulse);
    }
}
