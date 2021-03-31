using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beemon : Character<Beemon, BeemonState, BeemonStateInput>
{
    public float maxSpeed = 5f;
    public int attackRate = 1;
    public float attackStrength = 1.0f;
    public Transform waypoint_l;
    public Transform waypoint_r;


    protected override void Init()
    {
        stateInput.beemon = this;
        stateInput.anim = GetComponent<Animator>();
        stateInput.spriteRenderer = GetComponent<SpriteRenderer>();
        stateInput.rb = GetComponent<Rigidbody2D>();
        stateInput.boxCollider = GetComponent<BoxCollider2D>();
        stateInput.stateChanged = false;
        stateInput.gameobj = gameObject;
        stateInput.enemy_controller = GetComponent<EnemyController>();
    }
    protected override void SetInitialState()
    {
        ChangeState<BeemonWanderState>();
    }

}

public abstract class BeemonState : CharacterState<Beemon, BeemonState, BeemonStateInput>
{

}

public class BeemonStateInput : CharacterStateInput
{
    public Animator anim;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider;
    public bool stateChanged;
    public GameObject lastWall;
    public int lastXDir;
    public Beemon beemon;
    public GameObject gameobj;
    public EnemyController enemy_controller;
}
