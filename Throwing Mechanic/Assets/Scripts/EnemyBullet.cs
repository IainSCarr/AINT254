using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    public float speed = 5.0f;
    public float destroyTime = 3.0f;

    void OnEnable()
    {
        Invoke("Die", destroyTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.SendMessage("TakeDamage", 5f);
            collision.gameObject.SendMessage("DoDamageFlash");
            Die();
        }
    }

    void FixedUpdate()
    {
        // Fire towards Enemy
        //GetComponent<Rigidbody>().velocity = transform.forward * speed;
        transform.position = transform.forward * speed;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
