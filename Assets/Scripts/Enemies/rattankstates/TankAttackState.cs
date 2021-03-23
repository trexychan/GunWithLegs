using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAttackState : TankState
{
    public override void Enter(TankStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        Debug.Log("Entering Attack State");
        stateInput.anim.Play("Enemy_Attack");
    }

    public override void Update(TankStateInput stateInput)
    {
        
    }









    

    



    
}
