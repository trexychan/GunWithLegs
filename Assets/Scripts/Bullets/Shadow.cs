using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public float speed = 40f;
    public Rigidbody2D rb;
    // public GameObject impactEffect;

    void Start() //shoot shadow hand
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo) // shadow hands that hit enemies weaken them
    {
        if (hitInfo.gameObject.layer != 8) {
            // weaken the enemy
            EnemyController enemy = hitInfo.gameObject.GetComponent<EnemyController>();
            if (enemy)
            {
                enemy.TakeDamage(1);
                enemy.SetCondition(Condition.WEAK);
            }

            Destroy(gameObject);
        }
    }

    void OnBecameInvisible() // if rocket exits camera view, destroy itself
    {
        Destroy(gameObject);
    }
}
