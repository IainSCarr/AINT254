using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryController : MonoBehaviour {

    /// <summary>
    /// Number of dots in trajectory arc.
    /// </summary>
    public int resolution;

    /// <summary>
    /// Distance between dots based on time.
    /// </summary>
    public float spread;

    public GameObject dotPrefab;
    [SerializeField]
    private GameObject[] dots;

    private Camera cam;

    private AudioManager instance;

    private GameObject player;
    private Vector3 playerVector;

    private GameObject projectile;

    private Vector3 aimVector;
    private float magnitude;

    private bool hasClickedObject;
    private bool hasBeenThrown;

    // Use this for initialization
    void Start () {
        dots = new GameObject[resolution];

        // create array of dots and set to inactive
        for (int i = 0; i < resolution; i++)
        {
            GameObject tempDot = Instantiate(dotPrefab);

            tempDot.SetActive(false);

            dots[i] = tempDot;
        }

        cam = Camera.main;
        instance = AudioManager.instance;

        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        // if player is holding object
        if (projectile)
        {
            if (Input.GetMouseButtonDown(0))
            {
                hasClickedObject = GetHasClickedObject();
            }
            else if (Input.GetMouseButton(0))
            {
                if (hasClickedObject && !hasBeenThrown)
                {
                    CalculateVectors();
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (hasClickedObject && !hasBeenThrown)
                {
                    projectile.GetComponent<ThrowableBehaviour>().Throw(aimVector, magnitude);
                    instance.PlaySound("Throw");

                    hasBeenThrown = true;

                    for (int i = 0; i < resolution; i++)
                    {
                        dots[i].SetActive(false);
                    }
                }

                hasClickedObject = false;
            }
        }
    }

    /// <summary>
    /// Uses raycast to find if player has clicked on correct object.
    /// </summary>
    /// <returns>True if player has clicked on correct object.</returns>
    private bool GetHasClickedObject()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.name == player.name)
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

    /// <summary>
    /// Calculates vector and magnitude between mouse position and player.
    /// </summary>
    private void CalculateVectors()
    {
        playerVector = cam.WorldToScreenPoint(player.transform.position);
        playerVector.z = 0;

        aimVector = (playerVector - Input.mousePosition).normalized;
        aimVector.z = aimVector.y;
        magnitude = (playerVector - Input.mousePosition).magnitude * 0.1f;

        Aim(aimVector, magnitude);
    }

    /// <summary>
    /// Places dots in trajectory arc.
    /// </summary>
    /// <param name="direction">Direction of arc</param>
    /// <param name="magnitude">Amount of power</param>
    private void Aim(Vector3 direction, float magnitude)
    {
        float Sx = direction.x * magnitude;
        float Sy = direction.y * magnitude;

        for (int i = 0; i < resolution; i++)
        {
            float time = i * spread;

            float dX = Sx * time;
            float dY = (Sy * time) - (0.5f * (Physics.gravity.y * -1) * time * time);
            float dZ = Sy * time;

            Vector3 dotPosition = new Vector3(dX, dY, dZ);

            dots[i].SetActive(true);

            if (i != 0)
            {
                Color newColour = dots[i].GetComponent<Renderer>().material.color;
                newColour.a = ((float)resolution - i) / resolution;
                dots[i].GetComponent<Renderer>().material.color = newColour;
            }

            dots[i].transform.position = projectile.transform.position + dotPosition;
        }
    }

    public void SetProjectile(GameObject newProjectile)
    {
        projectile = newProjectile;
        hasBeenThrown = false;
        hasClickedObject = false;
    }
}
