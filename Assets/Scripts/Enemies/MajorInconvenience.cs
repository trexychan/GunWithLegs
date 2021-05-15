using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MajorInconvenience : Character<MajorInconvenience, MajorState, MajorStateInput>
{
    //general enemy stats
    public float moveSpeed = 2.0f;
    public int attackRate = 1;
    public float attackStrength = 1.0f;

    protected override void Init()
    {
        stateInput.major_inconvenience = this;
        stateInput.anim = GetComponent<Animator>();
        stateInput.spriteRenderer = GetComponent<SpriteRenderer>();
        stateInput.rb = GetComponent<Rigidbody2D>();
        stateInput.collider = GetComponent<BoxCollider2D>();
        stateInput.stateChanged = false;
        stateInput.gameobj = gameObject;
        stateInput.enemyController = GetComponent<EnemyController>();
        stateInput.player = GameObject.FindGameObjectWithTag("Player");
    }

    protected override void SetInitialState()
    {
        ChangeState<MajorSleepState>();
    }

    public MajorStateInput GetStateInput() {
        return stateInput;
    }

    public MajorState GetState() {
        return state;
    }
}

public abstract class MajorState : CharacterState<MajorInconvenience, MajorState, MajorStateInput>
{

}

public class MajorStateInput : CharacterStateInput
{
    public Animator anim;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public Collider2D collider;
    public bool stateChanged;
    public GameObject lastWall;
    public int lastXDir;
    public MajorInconvenience major_inconvenience;
    public GameObject gameobj;
    public EnemyController enemyController;
    public GameObject player;
}