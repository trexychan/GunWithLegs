using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public override void Enter(EnemyStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        base.Enter(stateInput, transitionInfo);
        // stateInput.anim.Play("Enemy_Idle");
    }

    public override void Update(EnemyStateInput stateInput)
    {
        base.Update(stateInput);
        
    }
}
