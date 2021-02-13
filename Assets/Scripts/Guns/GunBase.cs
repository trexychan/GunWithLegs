using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunBase : MonoBehaviour
{
    // Different guns consume your ammo at different rates. Some weapons might only take one bullet to fire a shot, but some may require several bullets to fire
    int shots_cost;
    Size size;
    float base_DMG;
    float fire_rate;


    public abstract void Shoot(); //handle

    public bool canDash()
    {
        if (size != Size.HEAVY) {return true;}
        else {return false;}
    }

    
}
public enum Size
{
    LIGHT,
    NORMAL,
    HEAVY
}