using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashDamage : MonoBehaviour
{
    int damage = 10;
    float collider_radius;
    void Start()
    {
        collider_radius = GetComponent<CircleCollider2D>().radius;
        Debug.Log(collider_radius);
    }

    void OnTriggerStay2D(Collider2D hitCollider) // damage enemy if it is within range of explosion
    {
        Debug.Log(hitCollider.gameObject.name);
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, collider_radius);

        foreach (Collider2D item in enemies)
        {
            Debug.Log(item.gameObject.name);
            EnemyController ec = item.GetComponent<EnemyController>();
            if (ec)
            {
                ec.TakeDamage(damage);
            }
        }
    }
}
