using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState {

    public override void Enter(PlayerStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        stateInput.lastXDir = 0;
        stateInput.anim.Play("Player_Idle");
    }

    public override void Update(PlayerStateInput stateInput)
    {
        stateInput.playerController.isGrounded = stateInput.playerController.checkIfGrounded();


        if (stateInput.playerController.canDash() && stateInput.playerControls.InGame.Dash.WasPressedThisFrame()) {
            character.ChangeState<PlayerDashState>();
            return;
        }

        if (stateInput.playerControls.InGame.SwitchLeft.WasPressedThisFrame()) {
            stateInput.playerController.switchGun(false);
        }

        if (stateInput.playerControls.InGame.SwitchRight.WasPressedThisFrame()) {
            stateInput.playerController.switchGun(true);
        }

        if (stateInput.playerControls.InGame.Shoot.WasPressedThisFrame() && stateInput.playerController.canFire) {
            stateInput.anim.Play("Player_Fire");
            stateInput.playerController.Shoot(); 
        }
        
        if (stateInput.playerControls.InGame.Jump.WasPressedThisFrame() && stateInput.playerController.canJump())
        {
            stateInput.playerController.Jump();
            character.ChangeState<PlayerJumpingState>();
            return;
        }
        if((stateInput.rb.velocity.y < 0) && !stateInput.playerController.isGrounded)
        {
            character.ChangeState<PlayerFallingState>();
            return;
        }
        // Movement animations and saving previous input
        int horizontalMovement = (int)Mathf.Sign(stateInput.playerControls.InGame.Move.ReadValue<Vector2>().x);
        if (stateInput.playerControls.InGame.Move.ReadValue<Vector2>().x > -0.1f && stateInput.playerControls.InGame.Move.ReadValue<Vector2>().x < 0.1f) {
            horizontalMovement = 0;
        }
        if (horizontalMovement != 0 && stateInput.lastXDir != horizontalMovement)
        {
            stateInput.player.transform.rotation = Quaternion.Euler(0, horizontalMovement == -1 ? 180 : 0, 0);
            // stateInput.spriteRenderer.flipX = horizontalMovement == -1;
        }
        stateInput.anim.SetFloat("speed", Mathf.Abs(horizontalMovement));
        stateInput.lastXDir = horizontalMovement;
    }


    public override void FixedUpdate(PlayerStateInput stateInput) {
        stateInput.playerController.HandleMovement();
    }

}
