﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    private float dashTimer;
    private int dir;
    public override void Enter(PlayerStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        dashTimer = stateInput.playerController.dashTime;
        dir = stateInput.player.transform.rotation.eulerAngles.y == 180 ? -1 : 1;
        stateInput.anim.Play("Player_Dash");
        stateInput.playerController.ExecuteDash();
    }
    
    public override void ForceCleanUp(PlayerStateInput stateInput)
    {
        stateInput.rb.velocity = Vector2.zero;
        stateInput.playerController.startDashCooldown();
    }

    public override void Update(PlayerStateInput stateInput)
    {
        if (dashTimer > 0)
        {
            stateInput.rb.velocity = new Vector2(dir * stateInput.playerController.dashSpeed, 0);
            dashTimer -= Time.deltaTime;
            if (stateInput.playerController.tookDamage()) {
            stateInput.playerController.setDamaged(false);
            Vector2 launchDirection = stateInput.playerController.launchVelocity;
            if (stateInput.player.transform.rotation.y == 0) {
                launchDirection.x = -launchDirection.x;
            }
            
            character.ChangeState<PlayerLaunchState>(new LaunchStateTransitionInfo(launchDirection, stateInput.playerController.moveAfterLaunchTime, true));
            return;
        }
        } else
        {
            stateInput.rb.velocity = Vector2.zero;
            character.ChangeState<PlayerFallingState>();
        }

        
    }
}
