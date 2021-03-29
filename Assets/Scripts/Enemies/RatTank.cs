using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatTank : Character<RatTank, TankState, TankStateInput>
{
    //general enemy stats
    public float moveSpeed = 1.0f;
    public int attackRate = 2;
    public float attackStrength = 1.0f;
    
    
    protected override void Init()
    {
        stateInput.rattank = this;
        stateInput.anim = GetComponent<Animator>();
        stateInput.spriteRenderer = GetComponent<SpriteRenderer>();
        stateInput.rb = GetComponent<Rigidbody2D>();
        stateInput.boxCollider = GetComponent<BoxCollider2D>();
        stateInput.stateChanged = false;
        stateInput.gameobj = gameObject;
        stateInput.enemy_controller = GetComponent<EnemyController>();
        stateInput.player = GameObject.FindGameObjectWithTag("Player");
    }

    protected override void SetInitialState()
    {
        ChangeState<TankIdleState>();
    }

    public TankStateInput GetStateInput() {
        return stateInput;
    }

    public TankState GetState() {
        return state;
    }

    
}

public abstract class TankState : CharacterState<RatTank, TankState, TankStateInput>
{

}

public class TankStateInput : CharacterStateInput
{
    public Animator anim;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider;
    public bool stateChanged;
    public GameObject lastWall;
    public int lastXDir;
    public RatTank rattank;
    public GameObject gameobj;
    public EnemyController enemy_controller;
    public GameObject player;
}
