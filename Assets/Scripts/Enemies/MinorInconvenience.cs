using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinorInconvenience : Character<MinorInconvenience, MinorState, MinorStateInput>
{
    //general enemy stats
    public float moveSpeed = 1.0f;
    public int attackRate = 1;
    public float attackStrength = 1.0f;
    
    protected override void Init()
    {
        stateInput.minor_inconvenience = this;
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
        ChangeState<MinorIdleState>();
    }

    public MinorStateInput GetStateInput() {
        return stateInput;
    }

    public MinorState GetState() {
        return state;
    }

}
public abstract class MinorState : CharacterState<MinorInconvenience, MinorState, MinorStateInput>
{

}

public class MinorStateInput : CharacterStateInput
{
    public Animator anim;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider;
    public bool stateChanged;
    public GameObject lastWall;
    public int lastXDir;
    public MinorInconvenience minor_inconvenience;
    public GameObject gameobj;
    public EnemyController enemy_controller;
    public GameObject player;
}
