using System.Runtime.CompilerServices;
using System;
using System.Globalization;
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
        if (transitionInfo == null) {
            timer = launchTime;
        } else {
            LaunchStateTransitionInfo launchTransition = (LaunchStateTransitionInfo) transitionInfo;
            timer = launchTransition.moveAfterLaunchTime;
            stateInput.playerController.launchPlayer(launchTransition.launchVelocity);
            stateInput.playerController.SetPlayerImmunity(launchTransition.invincible);
        }
        //stateInput.anim.Play("Player_Jump");
    }

    public override void Update(PlayerStateInput stateInput)
    {
        stateInput.playerController.isGrounded = stateInput.playerController.checkIfGrounded();
        Debug.Log(stateInput.rb.velocity);
        if (stateInput.playerController.isGrounded && timer <= 0) {
            character.ChangeState<PlayerIdleState>();
        }
        if (timer <= 0) {
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
            stateInput.lastXDir = horizontalMovement;
        }
        timer -= Time.deltaTime;
    }
    public override void FixedUpdate(PlayerStateInput stateInput)
    {
        if (false)//timer <= 0)
        {
            stateInput.playerController.HandleMovement();
        }
        

    }

    public override void ForceCleanUp(PlayerStateInput stateInput)
    {
    }

}

public class LaunchStateTransitionInfo : CharacterStateTransitionInfo
{
    public LaunchStateTransitionInfo(Vector2 launchVelocity, float moveTime, bool invincible) {
        this.launchVelocity = launchVelocity;
        this.moveAfterLaunchTime = moveTime;
        this.invincible = invincible;
    }
    public Vector2 launchVelocity;
    public float moveAfterLaunchTime;
    public bool invincible;

}
