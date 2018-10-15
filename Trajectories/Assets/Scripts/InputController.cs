using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MasterElement {

    private float smoothing = 0.05f;

    private bool hasClickedObject;
    private bool hasBeenThrown;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        app.model.player.MovePlayer(Input.GetAxis("Horizontal") * smoothing);

        if (Input.GetMouseButtonDown(0))
        {
            hasClickedObject = app.view.TargetClicked(Input.mousePosition, "Ingredient");
            

        }
        else if (Input.GetMouseButton(0))
        {
            if (hasClickedObject && !hasBeenThrown)
            {
                app.view.trajecView.DisplayTrajectory(Input.mousePosition);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (hasClickedObject && !hasBeenThrown)
            {
                app.view.trajecView.ThrowObject();

                hasBeenThrown = true;

                app.view.trajecView.HideTrajectory();
            }

            hasClickedObject = false;
            hasBeenThrown = false;

            app.model.currentObject.UpdateObject();
            app.model.UpdateObject();
        }
    }
}
