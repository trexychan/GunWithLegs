using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed = 3f;
    public float exp_radius = 1.8f;
    private float acceleration = 7f;
    public float maxSpeed = 25f;
    public int damage = 2; // the rocket technically doesn't deal damage, the explosion does, however direct impacts by the rocket will deal a lil bit of damage

    public Rigidbody2D rb;
    public GameObject impactEffect;

    void Start() // propel rocket forward
    {
        rb.velocity = transform.right * speed;
    }

    void Update()
    {
        if (rb.velocity.magnitude < maxSpeed) {rb.velocity = transform.right * (rb.velocity.magnitude + acceleration * Time.deltaTime);}
    }

    void OnTriggerEnter2D(Collider2D hitInfo) // if rocket hits something, explode
    {
        if (hitInfo.gameObject.layer != 8) {
            CamController.Instance.Shake(5, 0.3f);
            Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, exp_radius);

            foreach (Collider2D item in objects)
            {
                Debug.Log(item.gameObject.name);
                EnemyController ec = item.GetComponent<EnemyController>();
                if (ec)
                {
                    ec.TakeDamage(damage);
                }
            }
            Destroy(gameObject);
            Instantiate(impactEffect, transform.position, transform.rotation);
        }
    }

    void OnBecameInvisible() // if rocket exits camera view, destroy itself
    {
        Destroy(gameObject);
    }
}
