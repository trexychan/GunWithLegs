using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGWeapon : ProjectileGun
{
    bool shotFired = false; // bool to control fire delay

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !shotFired) { // if mouse is clicked, fire a shot
            shotFired = true;
            Shoot();
        }
    }

    public override void Shoot()
    {
        Instantiate(bullet, firePt.position, firePt.rotation);
        StartCoroutine(delayNextShot());
    }

    IEnumerator delayNextShot() 
    {
        yield return new WaitForSeconds(fireRate);
        shotFired = false;
    }
}
