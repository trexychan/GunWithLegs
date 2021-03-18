using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPG : ProjectileGun
{
    public RPG(StatePlayerController player, Transform firePoint, GameObject hitEffect, GameObject shotObj, AudioClip fireSound, RuntimeAnimatorController animatorController) {
        this.size = Size.HEAVY;
        this.shotCost = 10f;
        this.damage = 20;
        this.fireRate = 3f;
        this.firePt = firePoint;
        this.bullet = shotObj;
        this.bulletSpeed = 20f;
        this.hitEffect = hitEffect;
        this.fireSound = fireSound;
        this.player = player;
        this.animController = animatorController;
    }

    public override void Shoot()
    {
        Instantiate(bullet, firePt.position, firePt.rotation);
        player.playSound(fireSound);
        
    }

    
}
