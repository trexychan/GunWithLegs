using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPG : ProjectileGun
{
    private Vector2 boxSize;
    private Vector2 boxOffset;
    public RPG(StatePlayerController player, Transform firePoint, GameObject hitEffect, GameObject shotObj, AudioClip fireSound, RuntimeAnimatorController animatorController, Sprite icon) {
        this.size = Size.HEAVY;
        this.shotCost = 5f;
        this.damage = 10;
        this.fireRate = 3f;
        this.firePt = firePoint;
        this.bullet = shotObj;
        this.bulletSpeed = 20f;
        this.hitEffect = hitEffect;
        this.fireSound = fireSound;
        this.player = player;
        this.animController = animatorController;
        boxOffset = new Vector2(-0.09087026f, 0f);
        boxSize = new Vector2(1.417793f, 1.0625f);
        this.icon = icon;
        this.recoilVector = new Vector2(35f,0);
    }

    public override void Shoot()
    {
        Instantiate(bullet, firePt.position, firePt.rotation);
        player.DecreasePlayerCurrentHealth(shotCost);
        
    }

    public override void updateComponents()
    {
        player.boxCollider.size = boxSize;
        player.boxCollider.offset = boxOffset;
    }


}
