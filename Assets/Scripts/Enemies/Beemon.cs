using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beemon : Enemy
{
    public Beemon()
    {
        this.maxHealth = 3;
        this.currentHealth = this.maxHealth;
        this.moveSpeed = 2.0f;
        this.attackRate = 1.0f;
        this.attackStrength = 1;
        this.isAlive = true;
        this.enemyType = EnemyType.MELEE;
    }

    


}
