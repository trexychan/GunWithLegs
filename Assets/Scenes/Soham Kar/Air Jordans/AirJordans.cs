using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirJordans : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D hitCollider)
    {
        if (hitCollider.gameObject.tag == "Player") {
            hitCollider.gameObject.GetComponent<CharacterController2D>().setDoubleJump(true);
            Destroy(gameObject);
        }
    }
}
