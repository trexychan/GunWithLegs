using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankIdleState : TankState
{
    float timer = 0f;
    public override void Enter(TankStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        base.Enter(stateInput, transitionInfo);
        // stateInput.anim.Play("Enemy_Idle");
    }

    public override void Update(TankStateInput stateInput)
    {
        if (stateInput.anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack"))
        {
            // Debug.Log("attacking");
        } else
        {
            stateInput.anim.Play("Enemy_Idle");
            stateInput.enemy_controller.TurnToFacePlayer(stateInput.player.transform.position);
        }
        

        if (stateInput.enemy_controller.spottedPlayer())
        {
            stateInput.anim.Play("Enemy_Attack");
        }
    }

}
