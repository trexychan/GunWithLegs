using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : RaycastGun
{
    public int shotgunPellets = 5;
    public Shotgun(StatePlayerController player, Transform firePoint, GameObject hitEffect, AudioClip fireSound, LineRenderer renderer, RuntimeAnimatorController animatorController) {
        this.size = Size.NORMAL;
        this.shotCost = 4f;
        this.damage = 1;
        this.fireRate = 0.2f;
        this.firePt = firePoint;
        this.bulletTrail = renderer;
        this.hitEffect = hitEffect;
        this.fireSound = fireSound;
        this.player = player;
        this.animController = animatorController;
        this.maxRange = 10f;
    }

    // Update is called once per frame
    public override void Shoot()
    {
        for (int i=0; i<shotgunPellets; i++)
        {
            Vector3 temp = firePt.right;
            temp.y += Random.Range(-0.3f,0.3f);
            RaycastHit2D hitInfo = Physics2D.Raycast(firePt.position, temp);
            if (hitInfo)
            {
                bulletTrail.SetPosition(0,firePt.position);
                bulletTrail.SetPosition(1,hitInfo.point);

                GameObject new_hit = (GameObject)Instantiate(hitEffect, hitInfo.point, Quaternion.identity);
                Destroy(new_hit, 0.267f);

            } else 
            {
                
                bulletTrail.SetPosition(0,firePt.position);
                bulletTrail.SetPosition(1,firePt.position + temp*100);
            }
            player.showBulletTrail(bulletTrail);
        }
        player.playSound(fireSound);

    }
}
