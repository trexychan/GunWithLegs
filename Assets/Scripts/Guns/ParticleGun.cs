using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleGun : GunBase
{
    public ParticleSystem shot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Shoot()
    {
        shot.Play();
    }
}
