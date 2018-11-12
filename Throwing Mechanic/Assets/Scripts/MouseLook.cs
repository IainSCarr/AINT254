using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {
    private Camera cam;

    private Transform playerTransform;
    private Quaternion newRot;

    // Use this for initialization
    void Start () {
        cam = Camera.main;
        playerTransform = GetComponent<Transform>();
	}

    // speed is the rate at which the object will rotate
    public float speed;

    void FixedUpdate()
    {
        RotatePlayer();
        //playerTransform.eulerAngles.y = Mathf.Clamp(transform.eulerAngles.y, -90, 90);
        
    }

    /// <summary>
    /// Code used from http://wiki.unity3d.com/index.php?title=LookAtMouse
    /// </summary>
    private void RotatePlayer()
    {
        // Generate a plane that intersects the transform's position with an upwards normal.
        Plane playerPlane = new Plane(Vector3.up, playerTransform.position);

        // Generate a ray from the cursor position
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        // Determine the point where the cursor ray intersects the plane.
        // This will be the point that the object must look towards to be looking at the mouse.
        // Raycasting to a Plane object only gives us a distance, so we'll have to take the distance,
        //   then find the point along that ray that meets that distance.  This will be the point
        //   to look at.
        float hitdist = 0.0f;
        // If the ray is parallel to the plane, Raycast will return false.
        if (playerPlane.Raycast(ray, out hitdist))
        {
            // Get the point along the ray that hits the calculated distance.
            Vector3 targetPoint = ray.GetPoint(hitdist);

            // Determine the target rotation.  This is the rotation if the transform looks at the target point.
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - playerTransform.position);

            // Smoothly rotate towards the target point.
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, speed * Time.deltaTime);
        }
    }
}
