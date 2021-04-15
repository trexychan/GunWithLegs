using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StatePlayerController playerController = other.gameObject.GetComponent<StatePlayerController>();
            playerController.AddHealth(1f);
            Destroy(gameObject);
        }
    }
}
