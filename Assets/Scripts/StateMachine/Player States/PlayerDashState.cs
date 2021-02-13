using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    private float dashTimer;    
    public override void Enter(PlayerStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        //stateInput.canDash = false;
        //stateInput.playerController.dashing = true;
        dashTimer = stateInput.playerController.dashTime;
        stateInput.playerController.canDash = false;
    }

    public override void ForceCleanUp(PlayerStateInput stateInput)
    {
        stateInput.rb.velocity = Vector2.zero;
    }

    public override void Update(PlayerStateInput stateInput)
    {
        if (dashTimer > 0)
        {
            stateInput.rb.velocity = new Vector2(stateInput.lastXDir * stateInput.playerController.dashSpeed, 0);
            dashTimer -= Time.deltaTime;
        } else
        {
            stateInput.rb.velocity = Vector2.zero;
            //stateInput.playerController.dashing = false;
            character.ChangeState<PlayerFallingState>();
        }

        
    }
}
