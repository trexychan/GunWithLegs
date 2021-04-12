using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailGun : ProjectileGun
{
    public RailGun(StatePlayerController player, Transform firePt, GameObject hitEffect, GameObject shotObj, AudioClip fireSound, RuntimeAnimatorController animatorController,  Sprite icon) {
        size = Size.HEAVY;
        shotCost = 10f;
        damage = 10;
        fireRate = 1.0f;
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
        Instantiate(bullet, firePt.position, firePt.rotation);
        player.playSound(fireSound);
        player.DecreasePlayerCurrentHealth(shotCost);
        //StartCoroutine(ChargedShot());
        //enemy logic

        /* bulletTrail.SetPosition(0, firePt.position);
         bulletTrail.SetPosition(1, hitInfo.point);*/

        // GameObject hit_mark = (GameObject)Instantiate(hit_effect,hitInfo.point,Quaternion.identity);
        // Destroy(hit_mark,0.2f); // arbitrary delay on destroying effect, depends on if we have multiple hit effects of varying times
    }
}
