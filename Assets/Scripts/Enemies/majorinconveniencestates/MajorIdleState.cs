using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MajorIdleState : MajorState
{
    public override void Enter(MajorStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        base.Enter(stateInput, transitionInfo);
    }

    public override void Update(MajorStateInput stateInput)
    {
        stateInput.enemyController.TurnToFacePlayer(stateInput.player.transform.position);
    }
}
