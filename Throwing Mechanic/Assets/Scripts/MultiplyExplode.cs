using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplyExplode : MonoBehaviour {

    public GameObject exlodePrefab;

    private float power = 600f;

    private bool hasExploded;

    // set to true by default for non throwable objects
    private bool canExplode = true;

    private void Awake()
    {
        // if this script is attached to a throwable object set to false by default
        if (gameObject.GetComponent<ThrowableBehaviour>())
        {
            canExplode = false;
        }
    }

    public void Explode()
    {
        // if the object can explode
        if (canExplode)
        {
            // if it hasn't already exploded - this stops the same objects from multiplying more than once
            if (!hasExploded)
            {
                hasExploded = true;

                // create 5 gameobjects, place them so they don't interact with each other then add a force in the general direction they are placed
                for (int i = 0; i < 5; i++)
                {
                    GameObject tempObject = Instantiate(exlodePrefab, transform.position + (Vector3.up), transform.rotation);
                    tempObject.GetComponent<Collider>().material = GetComponent<Collider>().material;

                    if (i == 0)
                    {
                        tempObject.transform.position += (Vector3.forward * 0.5f) + (Vector3.right * 0.5f);
                        tempObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(0f, 1f), 0.9f, Random.Range(0f, 1f)) * power);
                    }
                    else if (i == 1)
                    {
                        tempObject.transform.position += (Vector3.forward * 0.5f) + (Vector3.right * -0.5f);
                        tempObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(0f, -1f), 0.6f, Random.Range(0f, 1f)) * power);
                    }
                    else if (i == 2)
                    {
                        tempObject.transform.position += (Vector3.forward * -0.5f) + (Vector3.right * 0.5f);
                        tempObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(0f, 1f), 0.6f, Random.Range(0f, -1f)) * power);
                    }
                    else if (i == 3)
                    {
                        tempObject.transform.position += (Vector3.forward * -0.5f) + (Vector3.right * -0.5f);
                        tempObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(0f, -1f), 0.6f, Random.Range(0f, -1f)) * power);
                    }
                    else if (i == 4)
                    {
                        tempObject.transform.position += Vector3.up;
                        tempObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1f, -1f), 0.6f, Random.Range(-1f, -1f)) * power);
                    }

                    Destroy(tempObject, 10f);
                }
            }
        }

    }

    /// <summary>
    /// Setter method for explode ability.
    /// </summary>
    /// <param name="setting"></param>
    public void SetCanExplode(bool setting)
    {
        canExplode = setting;
    }

    /// <summary>
    /// Get method for explode ability
    /// </summary>
    /// <returns></returns>
    public bool GetCanExplode()
    {
        return canExplode;
    }
}
