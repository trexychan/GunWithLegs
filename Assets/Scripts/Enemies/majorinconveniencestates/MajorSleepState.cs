using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MajorSleepState : MajorState
{
    // bosses have a sleep state that gets switched to idle when the player triggers their associated area

    public override void Enter(MajorStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        base.Enter(stateInput, transitionInfo);
        stateInput.anim.Play("major_sleep");
        stateInput.enemyController.isAwake = false;
    }

    public override void Update(MajorStateInput stateInput)
    {
        if (stateInput.enemyController.isAwake)
        {
            character.ChangeState<MajorIdleState>();
            return;
        }
        
    }
}
