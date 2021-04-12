using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Railgunbullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 2;
    public float exp_radius = 1.8f;
    //private float acceleration = 10f;
    public Rigidbody2D rb;
    public GameObject impactEffect;

    void Start() // propel rocket forward
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo) // if rocket hits something, explode
    {
        if (hitInfo.gameObject.layer != 8) {
            if (hitInfo.gameObject.layer != 19) {
                if (hitInfo.gameObject.layer == 11)
                {
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
                }
                else {
                    Destroy(gameObject);
                    Instantiate(impactEffect, transform.position, transform.rotation);
                }
            }
        }
    }

    void OnBecameInvisible() // if rocket exits camera view, destroy itself
    {
        Destroy(gameObject);
    }
}
