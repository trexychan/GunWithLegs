using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinorIdleState : MinorState
{
    public override void Enter(MinorStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        base.Enter(stateInput, transitionInfo);
        // stateInput.anim.Play("Enemy_Idle");
    }

    public override void Update(MinorStateInput stateInput)
    {
        stateInput.enemy_controller.TurnToFacePlayer(stateInput.player.transform.position);
    }
}
