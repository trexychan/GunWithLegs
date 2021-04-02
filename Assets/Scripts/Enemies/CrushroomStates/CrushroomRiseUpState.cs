using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushroomRiseUpState : CrushroomState
{
    float originalY;
    public override void Enter(CrushroomStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        stateInput.crushroomManager = stateInput.crushroom.GetComponent<CrushroomManager>();
        originalY = stateInput.crushroomManager.GetOriginalY();
    }

    public override void Update(CrushroomStateInput stateInput)
    {
       if (stateInput.crushroom.transform.position.y <= originalY) {
           stateInput.crushroom.transform.Translate(0f, 0.045f, 0f);
       } else {
           character.ChangeState<CrushroomIdleState>();
       }
    }
}
