using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public GameObject impactEffect;

    void Start() // propel rocket forward
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo) // if rocket hits something, explode
    {
        if (hitInfo.name != "Player") {
            Destroy(gameObject);
            Instantiate(impactEffect, transform.position, transform.rotation);
        }
    }

    void OnBecameInvisible() // if rocket exits camera view, destroy itself
    {
        Destroy(gameObject);
    }
}
