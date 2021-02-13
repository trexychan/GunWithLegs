using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaunchState : PlayerState
{
    private GameObject currentWall;
    private float launchTime = 1f;
    private float timer;

    public override void Enter(PlayerStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        timer = launchTime;
        //stateInput.anim.Play("Player_Jump");
    }

    public override void Update(PlayerStateInput stateInput)
    {
        stateInput.playerController.isGrounded = Physics2D.OverlapCircle(stateInput.playerController.groundCheck.position, stateInput.playerController.checkRadius, stateInput.playerController.whatIsGround);

        if (stateInput.playerController.isGrounded)
        {
            character.ChangeState<PlayerIdleState>();
        } else if (stateInput.rb.velocity.y <= 0)
        {
            character.ChangeState<PlayerFallingState>();
        }
        
        
        // Movement animations and saving previous input
        int horizontalMovement = (int)Mathf.Sign(stateInput.playerControls.InGame.Move.ReadValue<Vector2>().x);
        // if (InputMap.Instance.GetInput(ActionType.RIGHT))
        // {
        //     horizontalMovement++;
        // }
        // if (InputMap.Instance.GetInput(ActionType.LEFT))
        // {
        //     horizontalMovement--;
        // }
        if (stateInput.lastXDir != horizontalMovement)
        {
            if (horizontalMovement != 0)
            {
                //stateInput.spriteRenderer.flipX = horizontalMovement == -1;
                stateInput.lastXDir = horizontalMovement;
            }
        }
    }
    public override void FixedUpdate(PlayerStateInput stateInput)
    {
        if (timer >= 0)
        {
            stateInput.playerController.HandleLerpMovement();
        } else
        {
            stateInput.playerController.HandleMovement();
        }
        

    }

}
