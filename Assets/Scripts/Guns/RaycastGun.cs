using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastGun : GunBase
{
    public LineRenderer bulletTrail;
    public LayerMask layermask = 1 << 8 | 1 << 2;

    public override void Shoot()
    {
        //Damage player based on ammo cost
        Vector3 temp = firePt.right;
        temp.y += Random.Range(-0.1f,0.1f);
        RaycastHit2D hitInfo = Physics2D.Raycast(firePt.position, temp);
        Physics2D.IgnoreLayerCollision(8, Physics2D.IgnoreRaycastLayer);
        if (hitInfo)
        {
            //enemy logic

            bulletTrail.SetPosition(0, firePt.position);
            bulletTrail.SetPosition(1, hitInfo.point);

            GameObject hit_mark = (GameObject)Instantiate(hitEffect,hitInfo.point,Quaternion.identity);
            Destroy(hit_mark,0.2f); // arbitrary delay on destroying effect, depends on if we have multiple hit effects of varying times
            
        }
    }
}
