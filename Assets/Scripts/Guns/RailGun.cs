using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailGun : RaycastGun
{
    RailGun(StatePlayerController player, Transform firePt) {
        size = Size.HEAVY;
        shotCost = 10f;
        damage = 10;
        fireRate = 1.0f;
        this.firePt = firePt;
        this.player = player;
    }

    // The Rail gun fires train tracks in a straight line, travelling through multiple enemies
    public override void Shoot()
    {
        //Damage player based on ammo cost
        Vector3 temp = firePt.right;
        RaycastHit2D hitInfo = Physics2D.Raycast(firePt.position, temp);
        Physics2D.IgnoreLayerCollision(8, Physics2D.IgnoreRaycastLayer);
        Debug.Log(hitInfo);
        if (hitInfo)
        {
            //enemy logic

            bulletTrail.SetPosition(0, firePt.position);
            bulletTrail.SetPosition(1, hitInfo.point);

            // GameObject hit_mark = (GameObject)Instantiate(hit_effect,hitInfo.point,Quaternion.identity);
            // Destroy(hit_mark,0.2f); // arbitrary delay on destroying effect, depends on if we have multiple hit effects of varying times
            
        }
    }
}
