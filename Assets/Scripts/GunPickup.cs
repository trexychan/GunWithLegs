using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    public enum GunType {RPG, Shotgun, DualPistols}

    public GunType gunType;
    
    public void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.layer == 8) {
            StatePlayerController player = collider.GetComponent<StatePlayerController>();
            player.addGun((int)gunType);
            player.IncreaseMaxHealth(5);
            Destroy(gameObject);
        }
    }
}
