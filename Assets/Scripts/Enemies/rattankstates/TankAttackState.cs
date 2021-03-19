using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAttackState : TankState
{
    public override void Enter(TankStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        stateInput.anim.Play("Enemy_Spot");
    }

    



    
}
