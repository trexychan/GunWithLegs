using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character<Player, PlayerState, PlayerStateInput> {

	override protected void Init()
    {
        stateInput.stateMachine = this;
        stateInput.anim = GetComponent<Animator>();
        stateInput.spriteRenderer = GetComponent<SpriteRenderer>();
        stateInput.rb = GetComponent<Rigidbody2D>();
        stateInput.boxCollider = GetComponent<BoxCollider2D>();
        stateInput.playerController = GetComponent<StatePlayerController>();
        stateInput.stateChanged = false;
        stateInput.playerControls = stateInput.playerController.playerControls;
    }

    override protected void SetInitialState()
    {
        ChangeState<PlayerIdleState>();
    }

    public PlayerStateInput GetStateInput() {
        return stateInput;
    }

    public PlayerState GetState() {
        return state;
    }

}

public abstract class PlayerState : CharacterState<Player, PlayerState, PlayerStateInput>
{

}

public class PlayerStateInput : CharacterStateInput
{
    public Animator anim;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider;
    public StatePlayerController playerController;
    public bool stateChanged;
    public GameObject lastWall;
    public int lastXDir;
    public PlayerControls playerControls;
    public Camera playerCamera;
    public Player stateMachine;
}
