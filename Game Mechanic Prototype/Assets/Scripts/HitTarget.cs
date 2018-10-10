using UnityEngine;

public class HitTarget : MonoBehaviour {



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            collision.gameObject.SendMessage("ChangeColour");

            MultiplyExplode();
        }
    }

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void MultiplyExplode()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject tempObject = Instantiate(gameObject, transform.position + (transform.up), transform.rotation);

            if (i == 0)
            {
                tempObject.transform.position += (transform.forward * 0.5f) + (transform.right * 0.5f);
                tempObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(0f, 1f), 0.9f, Random.Range(0f, 1f)) * 30000f);
            }
            else if (i == 1)
            {
                tempObject.transform.position += (transform.forward * 0.5f) + (transform.right * -0.5f);
                tempObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(0f, -1f), 0.6f, Random.Range(0f, 1f)) * 30000f);
            }
            else if (i == 2)
            {
                tempObject.transform.position += (transform.forward * -0.5f) + (transform.right * 0.5f);
                tempObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(0f, 1f), 0.6f, Random.Range(0f, -1f)) * 30000f);
            }
            else if (i == 3)
            {
                tempObject.transform.position += (transform.forward * -0.5f) + (transform.right * -0.5f);
                tempObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(0f, -1f), 0.6f, Random.Range(0f, -1f)) * 30000f);
            }
            else if (i == 4)
            {
                tempObject.transform.position += transform.up;
                tempObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1f, -1f), 0.6f, Random.Range(-1f, -1f)) * 30000f);
            }

            Destroy(tempObject, 10f);
        }
    }
}
