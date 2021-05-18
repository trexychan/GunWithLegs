using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : RaycastGun
{
    public int shotgunPellets = 5;
    GameObject shot_trail;
    public Shotgun(StatePlayerController player, Transform firePoint, GameObject hitEffect, AudioClip fireSound, GameObject shot_trail, RuntimeAnimatorController animatorController, Sprite icon) {
        this.size = Size.NORMAL;
        this.shotCost = 3f;
        this.damage = 1;
        this.fireRate = 1f;
        this.firePt = firePoint;
        this.bulletTrail = shot_trail.GetComponent<LineRenderer>();
        this.shot_trail = shot_trail;
        this.hitEffect = hitEffect;
        this.fireSound = fireSound;
        this.player = player;
        this.animController = animatorController;
        this.maxRange = 3f;
        this.icon = icon;
        this.recoilVector = new Vector2(20f,0);
    }

    // Update is called once per frame
    public override void Shoot()
    {
        for (int i=0; i<shotgunPellets; i++)
        {
            GameObject temptrail = (GameObject)Instantiate(shot_trail, firePt.position, Quaternion.identity);
            LineRenderer temptrailrenderer = temptrail.GetComponent<LineRenderer>();
            Vector3 temp = firePt.right;
            temp.y += Random.Range(-0.3f,0.3f);
            RaycastHit2D hitInfo = Physics2D.Raycast(firePt.position, temp, maxRange, ~layermask);
            if (hitInfo)
            {
                temptrailrenderer.SetPosition(0,firePt.position);
                temptrailrenderer.SetPosition(1,hitInfo.point);

                EnemyController target = hitInfo.collider.gameObject.GetComponent<EnemyController>();
                if (target) {
                    target.TakeDamage(this.damage);
                    GameObject new_hit = (GameObject)Instantiate(hitEffect, hitInfo.point, Quaternion.identity);
                    Destroy(new_hit, 0.267f);
                } else {
                    Instantiate(player.deflectshotEffect,hitInfo.point,Quaternion.identity);
                }

            } else 
            {
                
                temptrailrenderer.SetPosition(0,firePt.position);
                temptrailrenderer.SetPosition(1,firePt.position + temp*maxRange);
            }
            Destroy(temptrail, 0.05f);
        }
        player.DecreasePlayerCurrentHealth(shotCost);

    }
}
