using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchObject : MonoBehaviour
{
    public GameObject projectile;

    private Camera cam;

    private Vector3 objectVector;
    private Transform objTrans;

    private Vector3 aimVector;
    private float magnitude;

    private bool hasClickedObject;
    private bool hasBeenThrown;

    void Start()
    {
        cam = Camera.main;

        objTrans = projectile.GetComponent<Transform>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "Cube")
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
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (hasClickedObject && !hasBeenThrown)
            {
                projectile.GetComponent<Rigidbody>().AddForce(aimVector * magnitude, ForceMode.Impulse);
                hasBeenThrown = true;
            }

            hasClickedObject = false;
        }
    }
}
