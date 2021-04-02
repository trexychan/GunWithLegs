using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crushroom : Character<Crushroom, CrushroomState, CrushroomStateInput>
{
    //general enemy stats
    public float moveSpeed = 1.0f;
    public int attackRate = 1;
    public float attackStrength = 1.0f;
    
    protected override void Init()
    {
        stateInput.crushroom = this;
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
        ChangeState<CrushroomIdleState>();
    }

    public CrushroomStateInput GetStateInput() {
        return stateInput;
    }

    public CrushroomState GetState() {
        return state;
    }
}
public abstract class CrushroomState : CharacterState<Crushroom, CrushroomState, CrushroomStateInput>
{

}

public class CrushroomStateInput : CharacterStateInput
{
    public Animator anim;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider;
    public bool stateChanged;
    public GameObject lastWall;
    public int lastXDir;
    public Crushroom crushroom;
    public GameObject gameobj;
    public EnemyController enemy_controller;
    public GameObject player;
    public CrushroomManager crushroomManager;
}