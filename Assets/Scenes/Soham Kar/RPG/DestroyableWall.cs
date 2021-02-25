using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableWall : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D hitCollider) // if hit by RPG rocket, destroy itself
    {
        Debug.Log(hitCollider.gameObject.tag);
        if (hitCollider.gameObject.tag == "Rocket") {
            Destroy(gameObject);
        }
    }
}
