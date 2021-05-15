using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MajorJumpState : MajorState
{
    public override void Enter(MajorStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        base.Enter(stateInput, transitionInfo);
        stateInput.anim.Play("major_jump");
        stateInput.enemyController.EnemyPause(1f);
    }

}
