using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashDamage : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D hitCollider) // damage enemy if it is within range of explosion
    {
        Debug.Log(hitCollider.gameObject.name);
    }
}
