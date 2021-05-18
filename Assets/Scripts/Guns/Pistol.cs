﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : RaycastGun
{
    
    public Pistol(StatePlayerController player, Transform firePoint, GameObject hitEffect, AudioClip fireSound, LineRenderer renderer, RuntimeAnimatorController animatorController, GameObject shell, Transform ejectPt, Sprite icon) {
        this.size = Size.LIGHT;
        this.shotCost = 1f;
        this.damage = 1;
        this.fireRate = 0.25f;
        this.firePt = firePoint;
        this.bulletTrail = renderer;
        this.hitEffect = hitEffect;
        this.fireSound = fireSound;
        this.player = player;
        this.animController = animatorController;
        this.maxRange = 9f;
        this.shell = shell;
        this.ejectPt = ejectPt;
        this.icon = icon;
        this.recoilVector = new Vector2(2f,0);

        roundPool = new Queue<GameObject>();
        for (int i = 0; i < 20; i++) {
            GameObject round = Instantiate(shell, ejectPt.position, Quaternion.identity);
            round.SetActive(false);
            roundPool.Enqueue(round);
        }
    }

    public override void Shoot()
    {
        Vector3 temp = new Vector3();
        temp = firePt.right;
        // temp.y += Random.Range(-0.1f,0.1f);

        RaycastHit2D hitInfo = Physics2D.Raycast(firePt.position, temp, maxRange, ~layermask);

        if (hitInfo)
        {
            Debug.Log(hitInfo.collider.name);
            bulletTrail.SetPosition(0,firePt.position);
            bulletTrail.SetPosition(1,hitInfo.point);

            
            
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
            bulletTrail.SetPosition(0,firePt.position);
            bulletTrail.SetPosition(1,firePt.position + temp*maxRange);
        }
        player.showBulletTrail(bulletTrail);
        ejectRound();
        player.DecreasePlayerCurrentHealth(shotCost);
        
    }

    public override void ejectRound()
    {
        //GameObject round_instance = Instantiate(shell,ejectPt.position,Quaternion.identity) as GameObject;
        GameObject spawn = roundPool.Dequeue();

        spawn.transform.position = ejectPt.position;
        spawn.transform.rotation = Quaternion.identity;
        spawn.SetActive(true);

        roundPool.Enqueue(spawn);
    }

    
}
