using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Railgunbullet : MonoBehaviour
{
    public float speed = 50f;
    public int damage = 5;
    public Rigidbody2D rb;
    public GameObject impactEffect;

    void Start() // propel rocket forward
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo) // if rocket hits something, explode
    {
        if (hitInfo.gameObject.layer == 11)
        {
            
            EnemyController ec = hitInfo.gameObject.GetComponent<EnemyController>();
            if (ec)
            {
                ec.TakeDamage(damage);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D hitInfo)
    {
        if (hitInfo.gameObject.layer == 9)
        {
            Destroy(gameObject);
            Instantiate(impactEffect, transform.position, transform.rotation);
        }
    }

}
