using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVGun : ProjectileGun
{
    public TVGun(StatePlayerController player, Transform firePoint, GameObject hitEffect, GameObject shotObj, AudioClip fireSound, RuntimeAnimatorController animatorController) {
        this.size = Size.NORMAL;
        this.shotCost = 2f;
        this.damage = 1;
        this.fireRate = 1.5f;
        this.firePt = firePoint;
        this.bullet = shotObj;
        this.bulletSpeed = 40f;
        this.hitEffect = hitEffect;
        this.fireSound = fireSound;
        this.player = player;
        this.animController = animatorController;
    }

    public override void Shoot()
    {
        Instantiate(bullet, firePt.position, firePt.rotation);
        // player.playSound(fireSound);
        
    }

    
}