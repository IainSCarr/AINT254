using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplyExplode : MonoBehaviour {

    private float power = 600f;

    private bool hasExploded;

    private void Explode()
    {

       // if (!hasExploded)
       // {
       //     hasExploded = true;
            for (int i = 0; i < 5; i++)
            {
                GameObject tempObject = Instantiate(gameObject, transform.position + (Vector3.up), transform.rotation);

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
      //  }
    }
}
