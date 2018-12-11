using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    public float speed = 10.0f;
    public float destroyTime = 3.0f;
    private float dampner = 0.01f;

    private Vector3 direction;

    void OnEnable()
    {
        Invoke("Die", destroyTime);
        direction = transform.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.SendMessage("TakeDamage", 5f);
            other.gameObject.SendMessage("DoDamageFlash");
            Die();
        }
        else if (other.gameObject.tag == "Obstacle")
        {
            Die();
        }
    }

    void FixedUpdate()
    {
        // Fire towards Enemy
        //GetComponent<Rigidbody>().velocity = transform.forward * speed;
        transform.position += direction * speed * dampner;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
