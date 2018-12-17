using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class LaunchArcMesh : MonoBehaviour {

    private Camera cam;

    private AudioManager instance;

    private GameObject player;
    private Vector3 playerVector;

    private GameObject projectile;

    private Vector3 mousePos;

    private Vector3 aimVector;
    private float magnitude;

    private bool hasBeenThrown;

    // Update is called once per frame
    void Update()
    {
        // if player is holding object
        //if (projectile)
        //{
            if (Input.GetMouseButtonDown(0))
            {
                mousePos = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                if (!hasBeenThrown)
                {
                    CalculateVectors();
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (!hasBeenThrown)
                {
                    projectile.GetComponent<ThrowableBehaviour>().Throw(aimVector, magnitude);
                    instance.PlaySound("Throw");

                    hasBeenThrown = true;
                }

                //hasClickedObject = false;
            }
        //}
    }

    /// <summary>
    /// Calculates vector and magnitude between mouse position and player.
    /// </summary>
    private void CalculateVectors()
    {
        Debug.Log("Calculating");
        playerVector = cam.WorldToScreenPoint(player.transform.position);
        playerVector.z = 0;

        aimVector = (Input.mousePosition - mousePos).normalized;
        aimVector.z = aimVector.y;
        magnitude = (Input.mousePosition - mousePos).magnitude * 0.1f;

        MakeArcMesh(CalculateArcArray(aimVector, magnitude));
    }



    Mesh mesh;

    public float meshWidth;
    public int resolution;

    public float velocity;
    public float angle;

    float g;
    float radianAngle;

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        g = Mathf.Abs(Physics2D.gravity.y);
    }

    private void OnValidate()
    {
        if (mesh != null && Application.isPlaying)
        {
            //MakeArcMesh(CalculateArcArray());
        }
    }

    // Use this for initialization
    void Start () {
        cam = Camera.main;
        instance = AudioManager.instance;

        player = GameObject.FindGameObjectWithTag("Player");

        //MakeArcMesh(CalculateArcArray());
	}

    public void MakeArcMesh(Vector3[] arcVerts)
    {
        mesh.Clear();

        Vector3[] vertices = new Vector3[(resolution + 1) * 2];
        int[] triangles = new int[resolution * 12];

        for (int i = 0; i <= resolution; i++)
        {
            vertices[i * 2] = new Vector3(meshWidth * 0.5f, arcVerts[i].y, arcVerts[i].x);
            vertices[i * 2 + 1] = new Vector3(meshWidth * -0.5f, arcVerts[i].y, arcVerts[i].x);

            if (i != resolution)
            {
                triangles[i * 12] = i * 2;
                triangles[i * 12 + 1] = triangles[i * 12 + 4] = i * 2 + 1;
                triangles[i * 12 + 2] = triangles[i * 12 + 3] = (i + 1) * 2 ;
                triangles[i * 12 + 5] = (i + 1) * 2 + 1;

                triangles[i * 12 + 6] = i * 2;
                triangles[i * 12 + 7] = triangles[i * 12 + 10] = (i + 1) * 2;
                triangles[i * 12 + 8] = triangles[i * 12 + 9] = i * 2 + 1;
                triangles[i * 12 + 11] = (i + 1) * 2 + 1;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }

    Vector3[] CalculateArcArray(Vector3 aimVector, float velocity)
    {
        Vector3[] arcArray = new Vector3[resolution + 1];

        radianAngle = Mathf.Deg2Rad * angle;
        float maxDistance = (velocity * velocity * Mathf.Sin(2 * radianAngle)) / g;

        for (int i = 0; i <= resolution; i++)
        {
            float t = (float)i / (float)resolution;
            arcArray[i] = CalculateArcPoint(t, maxDistance, velocity, aimVector);
        }

        return arcArray;
    }

    Vector3 CalculateArcPoint(float t, float maxDistance, float velocity, Vector3 direction)
    {
        //float x = t * maxDistance;
        //float y = x * Mathf.Tan(radianAngle) - ((g * x * x) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));
        //float z = y * t;
        //return new Vector3(x, y, z);

        float Sx = direction.x * velocity;
        float Sy = direction.y * velocity;

        float time = t;

        float dX = Sx * time;
        float dY = (Sy * time) - (0.5f * (Physics.gravity.y * -1) * time * time);
        float dZ = Sy * time;

        return new Vector3(dX, dY, dZ);
    }
}
