using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowTrajectory : MonoBehaviour {

    public GameObject dotPrefab;
    [SerializeField]
    private GameObject[] dots;

    private Camera cam;

    public GameObject throwable;
    private Vector3 objectVector;



    // Use this for initialization
    void Start () {
        dots = new GameObject[10];

        for (int i = 0; i < 10; i++)
        {
            GameObject tempDot = Instantiate(dotPrefab);

            tempDot.SetActive(false);

            dots[i] = tempDot;

            cam = Camera.main;
        }
    }
	
	void Update () {
        objectVector = cam.WorldToScreenPoint(throwable.transform.position);
	}
}
