using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualPistols : RaycastGun
{
    private IEnumerator coroutine;
    private Transform secondFirePoint;
    private LineRenderer bulletTrailLeft;

    public DualPistols(StatePlayerController player, Transform firstFirePoint, Transform secondFirePoint, GameObject hitEffect, AudioClip fireSound, LineRenderer rendererRight, LineRenderer rendererLeft, RuntimeAnimatorController animatorController) {
        this.size = Size.LIGHT;
        this.shotCost = 2f;
        this.damage = 1;
        this.fireRate = 0.2f;
        this.firePt = firstFirePoint;
        this.secondFirePoint = secondFirePoint;
        this.bulletTrail = rendererRight;
        this.bulletTrailLeft = rendererLeft;
        this.hitEffect = hitEffect;
        this.fireSound = fireSound;
        this.player = player;
        this.animController = animatorController;
        this.maxRange = 10f;
    }

    public override void Shoot()
    {
        Vector3 temp1 = new Vector3();
        Vector3 temp2 = new Vector3();
        temp1 = firePt.right;
        temp2 = -(secondFirePoint.right);
        // temp.y += Random.Range(-0.1f,0.1f);

        RaycastHit2D hitInfoRight = Physics2D.Raycast(firePt.position, temp1, maxRange);
        RaycastHit2D hitInfoLeft = Physics2D.Raycast(secondFirePoint.position, temp2, maxRange);
        Physics2D.IgnoreLayerCollision(8, Physics2D.IgnoreRaycastLayer);

        if (hitInfoRight)
        {
            bulletTrail.SetPosition(0,firePt.position);
            bulletTrail.SetPosition(1,hitInfoRight.point);

            GameObject new_hit = (GameObject)Instantiate(hitEffect, hitInfoRight.point, Quaternion.identity);
            Destroy(new_hit, 0.267f);

            Debug.Log(hitInfoRight.transform.name);

        } else 
        {
            bulletTrail.SetPosition(0,firePt.position);
            bulletTrail.SetPosition(1,firePt.position + temp1*maxRange);
        }

        if (hitInfoLeft)
        {
            bulletTrailLeft.SetPosition(0,secondFirePoint.position);
            bulletTrailLeft.SetPosition(1,hitInfoLeft.point);

            GameObject new_hit = (GameObject)Instantiate(hitEffect, hitInfoLeft.point, Quaternion.identity);
            Destroy(new_hit, 0.267f);

            Debug.Log(hitInfoLeft.transform.name);

        } else 
        {
            bulletTrailLeft.SetPosition(0,secondFirePoint.position);
            bulletTrailLeft.SetPosition(1,secondFirePoint.position + temp2*maxRange);
        }
        player.playSound(fireSound);
        player.showBulletTrail(bulletTrail);
        player.showBulletTrail(bulletTrailLeft);
    }
}
