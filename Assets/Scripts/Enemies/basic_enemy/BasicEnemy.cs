using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    public BasicEnemy(int hp, float movespd, float attackspd, int attackstr)
    {
        this.maxHealth = hp;
        this.currentHealth = this.maxHealth;
        this.moveSpeed = movespd;
        this.attackRate = attackspd;
        this.attackStrength = attackstr;
        this.enemyType = EnemyType.PASSIVE;
    }

}

public class BasicEnemyStateInput : EnemyStateInput
{

}
