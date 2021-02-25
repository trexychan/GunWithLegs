using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : RaycastGun
{
    public int shotgunPellets = 5;
    // Start is called before the first frame update
    void Start()
    {
        
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
