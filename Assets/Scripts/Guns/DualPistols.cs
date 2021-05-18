using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualPistols : RaycastGun
{
    private IEnumerator coroutine;
    private Transform secondFirePoint;
    private LineRenderer bulletTrailLeft;

    public DualPistols(StatePlayerController player, Transform firstFirePoint, Transform secondFirePoint, GameObject hitEffect, AudioClip fireSound, LineRenderer rendererRight, LineRenderer rendererLeft, RuntimeAnimatorController animatorController,  GameObject shell, Transform ejectPt, Sprite icon) {
        this.size = Size.LIGHT;
        this.shotCost = 1f;
        this.damage = 1;
        this.fireRate = 0.2f;
        this.firePt = firstFirePoint;
        this.secondFirePoint = secondFirePoint;
        this.bulletTrail = rendererRight;
        this.bulletTrailLeft = rendererLeft;
        this.hitEffect = hitEffect;
        this.fireSound = fireSound;
        this.ejectPt = ejectPt;
        this.player = player;
        this.shell = shell;
        this.animController = animatorController;
        this.maxRange = 8f;
        this.icon = icon;
        this.recoilVector = new Vector2(1f,0);

        roundPool = new Queue<GameObject>();
        for (int i = 0; i < 20; i++) {
            GameObject round = Instantiate(shell, ejectPt.position, Quaternion.identity);
            round.SetActive(false);
            roundPool.Enqueue(round);
        }
    }

    public override void Shoot()
    {
        Vector3 temp1 = new Vector3();
        Vector3 temp2 = new Vector3();
        temp1 = firePt.right;
        temp2 = -(secondFirePoint.right);
        // temp.y += Random.Range(-0.1f,0.1f);

        RaycastHit2D hitInfoRight = Physics2D.Raycast(firePt.position, temp1, maxRange, ~layermask);
        RaycastHit2D hitInfoLeft = Physics2D.Raycast(secondFirePoint.position, temp2, maxRange/2, ~layermask);
        // Physics2D.IgnoreLayerCollision(8, Physics2D.IgnoreRaycastLayer);

        if (hitInfoRight)
        {
            bulletTrail.SetPosition(0,firePt.position);
            bulletTrail.SetPosition(1,hitInfoRight.point);


            EnemyController target = hitInfoRight.collider.gameObject.GetComponent<EnemyController>();
            if (target) {
                target.TakeDamage(this.damage);
                GameObject new_hit = (GameObject)Instantiate(hitEffect, hitInfoRight.point, Quaternion.identity);
                Destroy(new_hit, 0.267f);
            } else {
                Instantiate(player.deflectshotEffect,hitInfoRight.point,Quaternion.identity);
            }

        } else 
        {
            bulletTrail.SetPosition(0,firePt.position);
            bulletTrail.SetPosition(1,firePt.position + temp1*maxRange);
        }

        if (hitInfoLeft)
        {
            bulletTrailLeft.SetPosition(0,secondFirePoint.position);
            bulletTrailLeft.SetPosition(1,hitInfoLeft.point);

            EnemyController target = hitInfoLeft.collider.gameObject.GetComponent<EnemyController>();
            if (target) {
                target.TakeDamage(this.damage);
                GameObject new_hit = (GameObject)Instantiate(hitEffect, hitInfoLeft.point, Quaternion.identity);
                Destroy(new_hit, 0.267f);
            } else {
                Instantiate(player.deflectshotEffect,hitInfoLeft.point,Quaternion.identity);
            }

        } else 
        {
            bulletTrailLeft.SetPosition(0,secondFirePoint.position);
            bulletTrailLeft.SetPosition(1,secondFirePoint.position + temp2*(maxRange/2));
        }
        player.showBulletTrail(bulletTrail);
        player.showBulletTrail(bulletTrailLeft);
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
