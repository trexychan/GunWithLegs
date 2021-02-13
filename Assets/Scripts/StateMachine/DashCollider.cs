using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashCollider : MonoBehaviour
{
    public StatePlayerController playerController;
    
    void OnTriggerEnter2D(Collider2D collider) {
        playerController.checkDashCollision(collider);
    }
}
