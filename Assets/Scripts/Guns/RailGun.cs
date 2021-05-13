using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailGun : ProjectileGun
{
    public RailGun(StatePlayerController player, Transform firePt, GameObject hitEffect, GameObject shotObj, AudioClip fireSound, RuntimeAnimatorController animatorController,  Sprite icon) {
        size = Size.HEAVY;
        shotCost = 6f;
        damage = 8;
        fireRate = 1.4f;
        this.firePt = firePt;
        this.hitEffect = hitEffect;
        this.bullet = shotObj;
        this.bulletSpeed = 20f;
        this.fireSound = fireSound;
        this.player = player;
        this.animController = animatorController;
        this.icon = icon;
        this.charge = .5f;
    }
    
    // The Rail gun fires train tracks in a straight line, travelling through multiple enemies
    public override void Shoot()
    {
        //Damage player based on ammo cost
        Instantiate(this.bullet, firePt.position, firePt.rotation);
        player.playSound(fireSound);
        player.DecreasePlayerCurrentHealth(shotCost);
        
    }
}
