using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankIdleState : TankState
{
    public override void Enter(TankStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        base.Enter(stateInput, transitionInfo);
        // stateInput.anim.Play("Enemy_Idle");
    }

    public override void Update(TankStateInput stateInput)
    {
        stateInput.enemy_controller.TurnToFacePlayer(stateInput.player.transform.position);

        if (stateInput.enemy_controller.spottedPlayer())
        {
            if (timer > stateInput.rattank.attackRate)
            stateInput.anim.Play("Enemy_Attack");
            
        }
    }
}
