using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunBase : MonoBehaviour
{
    // Different guns consume your ammo at different rates. Some weapons might only take one bullet to fire a shot, but some may require several bullets to fire
    public float shotCost;
    public Size size;
    public int damage;
    public float maxRange;
    public float fireRate;
    public RuntimeAnimatorController animController;
    public Transform firePt;
    public StatePlayerController player;
    public GameObject hitEffect;
    public AudioClip fireSound;
    public GameObject shell;
    public Transform ejectPt;
    // public bool canFire;


    public abstract void Shoot(); //handle

    public bool canDash()
    {
        if (size != Size.HEAVY) {return true;}
        else {return false;}
    }

    public virtual void updateComponents() {
        
    }

    
}
public enum Size
{
    LIGHT,
    NORMAL,
    HEAVY
}