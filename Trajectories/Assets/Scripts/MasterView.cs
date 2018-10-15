using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Contains all views related to the app.
public class MasterView : MasterElement {

    public TrajectoryView trajecView;

    public Camera cam;

    void Start() {
        
    }

    public bool TargetClicked(Vector3 mousePosition, string target)
    {
        Ray ray = cam.ScreenPointToRay(mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == target)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
