using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MajorInconvenience : Character<MajorInconvenience, MajorState, MajorStateInput>
{
    //general enemy stats
    public float moveSpeed = 2.0f;
    public int attackRate = 1;
    public float attackStrength = 1.0f;
    public bool hasJumped = false;

    protected override void Init()
    {
        stateInput.major_inconvenience = this;
        stateInput.anim = GetComponent<Animator>();
        stateInput.spriteRenderer = GetComponent<SpriteRenderer>();
        stateInput.rb = GetComponentInParent<Rigidbody2D>();
        stateInput.collider = GetComponentInParent<BoxCollider2D>();
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

    public void JumpAttack()
    {
        hasJumped = true;
        float distanceFromPlayer = stateInput.player.transform.position.x - stateInput.gameobj.transform.position.x;
        if (stateInput.enemyController.checkIsGrounded())
        {
            Debug.Log("jumped");
            stateInput.rb.AddForce(new Vector2(distanceFromPlayer, 7f), ForceMode2D.Impulse);
        }
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