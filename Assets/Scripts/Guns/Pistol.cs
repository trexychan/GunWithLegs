using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : RaycastGun
{
    private IEnumerator coroutine;
    public Pistol(StatePlayerController player, Transform firePoint, GameObject hitEffect, AudioClip fireSound, LineRenderer renderer, RuntimeAnimatorController animatorController, GameObject shell, Transform ejectPt) {
        this.size = Size.LIGHT;
        this.shotCost = 1;
        this.damage = 1;
        this.fireRate = 0.2f;
        this.firePt = firePoint;
        this.bulletTrail = renderer;
        this.hitEffect = hitEffect;
        this.fireSound = fireSound;
        this.player = player;
        this.animController = animatorController;
        this.maxRange = 9f;
        this.shell = shell;
        this.ejectPt = ejectPt;
    }

    public override void Shoot()
    {
        Vector3 temp = new Vector3();
        temp = firePt.right;
        // temp.y += Random.Range(-0.1f,0.1f);

        RaycastHit2D hitInfo = Physics2D.Raycast(firePt.position, temp, maxRange);
        Physics2D.IgnoreLayerCollision(8, Physics2D.IgnoreRaycastLayer);

        if (hitInfo)
        {
            bulletTrail.SetPosition(0,firePt.position);
            bulletTrail.SetPosition(1,hitInfo.point);

            GameObject new_hit = (GameObject)Instantiate(hitEffect, hitInfo.point, Quaternion.identity);
            Destroy(new_hit, 0.267f);
            
            EnemyController target = hitInfo.collider.gameObject.GetComponent<EnemyController>();
            if (target) {
                target.TakeDamage(this.damage);
            }
            


        } else 
        {
            bulletTrail.SetPosition(0,firePt.position);
            bulletTrail.SetPosition(1,firePt.position + temp*maxRange);
        }
        player.playSound(fireSound);
        player.showBulletTrail(bulletTrail);
        ejectRound();
        
    }

    private void ejectRound()
    {
        GameObject round_instance = Instantiate(shell,ejectPt.position,Quaternion.identity) as GameObject;
    }

    
}
