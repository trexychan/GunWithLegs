using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Bullet
{
    public float speed = 6f;
    public Rigidbody2D rb;
    public GameObject impacteffect;
    public float projectile_damage;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = -(transform.right * speed);
    }

    void OnTriggerEnter2D(Collider2D hitInfo) // if rocket hits something, explode
    {
        if (hitInfo.gameObject.layer == 9) {
            Destroy(gameObject);
            Instantiate(impacteffect, transform.position, transform.rotation);
        }
    }

    public float GetProjectileDamage()
    {
        return projectile_damage;
    }
}
