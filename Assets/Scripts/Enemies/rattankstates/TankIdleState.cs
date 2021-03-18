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
}
