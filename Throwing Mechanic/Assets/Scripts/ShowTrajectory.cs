﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTrajectory : MonoBehaviour {

    public GameObject dotPrefab;
    [SerializeField]
    private GameObject[] dots;

    private Camera cam;

    [SerializeField]
    private GameObject projectile;

    private Vector3 objectVector;
    private Transform objTrans;

    private Vector3 aimVector;
    private float magnitude;

    private bool hasClickedObject;
    private bool hasBeenThrown;

    void Start () {
        dots = new GameObject[10];

        for (int i = 0; i < 10; i++)
        {
            GameObject tempDot = Instantiate(dotPrefab);

            tempDot.SetActive(false);

            dots[i] = tempDot;
        }

        cam = Camera.main;

        objTrans = projectile.GetComponent<Transform>();
    }
	
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == projectile.name)
                {
                    hasClickedObject = true;
                }
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (hasClickedObject && !hasBeenThrown)
            {
                objectVector = cam.WorldToScreenPoint(objTrans.position);
                objectVector.z = 0;

                aimVector = (objectVector - Input.mousePosition).normalized;
                aimVector.z = aimVector.y;
                magnitude = (objectVector - Input.mousePosition).magnitude * 0.1f;

                Aim(aimVector, magnitude);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (hasClickedObject && !hasBeenThrown)
            {
                projectile.GetComponent<Rigidbody>().AddForce(aimVector * magnitude, ForceMode.Impulse);
                projectile.GetComponent<Rigidbody>().AddTorque(new Vector3(1, 0, -aimVector.x) *magnitude);
                hasBeenThrown = true;

                for (int i = 0; i < 10; i++)
                {
                    dots[i].SetActive(false);
                }
            }

            hasClickedObject = false;
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
            float dY = (Sy * time) - (0.5f * (Physics.gravity.y*-1) * time * time);
            float dZ = Sy * time;

            Vector3 dotPosition = new Vector3(dX, dY, dZ);

            dots[i].SetActive(true);

            if (i != 0)
            {
                Color newColour = dots[i].GetComponent<Renderer>().material.color;
                newColour.a = 1f / time*0.4f;
                dots[i].GetComponent<Renderer>().material.color = newColour;
            }

            dots[i].transform.position = objTrans.position + dotPosition;
        }
    }

    private void Reset()
    {
        hasBeenThrown = false;
        hasClickedObject = false;
    }

    public bool GetHasBeenThrown()
    {
        return hasBeenThrown;
    }

    public void SetProjectile(GameObject projec)
    {
        projectile = projec;
        objTrans = projectile.GetComponent<Transform>();
    }
}
