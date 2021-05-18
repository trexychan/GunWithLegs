using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PythonGun : ProjectileGun
{
    public PythonGun(StatePlayerController playerController, Transform firePt, GameObject hitEffect, GameObject projectile, AudioClip fireSound, RuntimeAnimatorController animatorController, Sprite icon)
    {
        size = Size.LIGHT;
        shotCost = 2f;
        damage = 2; // damage per snake segment explosion
        fireRate = 5f; // max snake length
        this.firePt = firePt;
        this.hitEffect = hitEffect;
        this.bullet = projectile;
        this.bulletSpeed = 10f; //interval (seconds) between snake head moving
        this.fireSound = fireSound;
        this.player = playerController;
        this.animController = animatorController;
        this.icon = icon;
    }

    // The Rail gun fires train tracks in a straight line, travelling through multiple enemies
    public override void Shoot()
    {
        //Spawn the head of the snake
        GameObject head = Instantiate(this.bullet, firePt.position, firePt.rotation) as GameObject;
        for (int i = 0; i < fireRate; i++)
        {
            Vector3 bulletpos = new Vector3 (head.transform.position.x - (0.5f * i), head.transform.position.y, 0);
            Instantiate(this.bullet, bulletpos, firePt.rotation);

        }
        player.DecreasePlayerCurrentHealth(shotCost);
        
    }
}
