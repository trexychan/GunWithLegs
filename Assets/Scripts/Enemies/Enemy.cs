using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character<Enemy, EnemyState, EnemyStateInput>
{
    //general enemy stats
    public int maxHealth = 1;
    public int currentHealth;
    public float moveSpeed = 1.0f;
    public float attackRate = 1.0f;
    public int attackStrength = 1;
    public bool isAlive = true;
    public EnemyType enemyType;
    
    protected override void Init()
    {
        stateInput.stateMachine = this;
        stateInput.anim = GetComponent<Animator>();
        stateInput.spriteRenderer = GetComponent<SpriteRenderer>();
        stateInput.rb = GetComponent<Rigidbody2D>();
        stateInput.boxCollider = GetComponent<BoxCollider2D>();
        stateInput.stateChanged = false;
        stateInput.enemy = gameObject;
        this.currentHealth = maxHealth;
    }

    protected override void SetInitialState()
    {
        ChangeState<EnemyIdleState>();
    }

    public EnemyStateInput GetStateInput() {
        return stateInput;
    }

    public EnemyState GetState() {
        return state;
    }

    public bool IsAlive() {
        return this.isAlive;
    }

    public virtual void Attack() {

    }

    public void TurnToFacePlayer(Transform pos) {
        if (pos.transform.position.x > this.gameObject.transform.position.x) {
            this.gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        } else {
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public virtual void Die() {
        this.gameObject.SetActive(false);
    }

    public void TakeDamage(int damage) {
        this.currentHealth -= damage;
        if (this.currentHealth <= 0) {
            Die();
        }
    }
}

public abstract class EnemyState : CharacterState<Enemy, EnemyState, EnemyStateInput>
{

}

public class EnemyStateInput : CharacterStateInput
{
    public Animator anim;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider;
    public bool stateChanged;
    public GameObject lastWall;
    public int lastXDir;
    public Enemy stateMachine;
    public GameObject enemy;
}

public enum EnemyType
{
    PASSIVE,
    MELEE,
    RANGED,
    BOSS
}
