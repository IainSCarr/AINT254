using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrows : MonoBehaviour {

    public Transform player;

    private Renderer rend;
    private Vector3 rightOffset;
    private Vector3 leftOffset;

	// Use this for initialization
	void Start () {
        rend = GetComponent<SpriteRenderer>();
        rightOffset = new Vector3(5, -player.transform.localScale.y/2 + 0.01f, 0);
        leftOffset = new Vector3(-5, -player.transform.localScale.y/2 + 0.01f, 0);
        StartCoroutine(Disappear());
    }

    // Update is called once per frame
    void Update () {
        if (transform.name == "Right")
        {
            transform.position = player.position + rightOffset;
        }
        else
        {
            transform.position = player.position + leftOffset;
        }
    }

    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(3f);
        float alpha = rend.material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 2)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, 0f, t));
            rend.material.color = newColor;
            yield return null;
        }
        Destroy(gameObject);
    }
}
