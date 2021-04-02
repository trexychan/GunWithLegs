using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushroomFallDownState : CrushroomState
{
    bool falling;
    
    public override void Enter(CrushroomStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        stateInput.crushroomManager = stateInput.crushroom.GetComponent<CrushroomManager>();
        stateInput.crushroom.GetComponent<Rigidbody2D>().bodyType =  RigidbodyType2D.Dynamic;
        stateInput.crushroom.GetComponent<Rigidbody2D>().gravityScale =  2f;
        falling = true;
    }

    public override void Update(CrushroomStateInput stateInput)
    {
        if (stateInput.crushroom.GetComponent<Rigidbody2D>().IsSleeping() && falling) { // if the crushroom hits the ground
            falling = false;
            stateInput.crushroom.GetComponent<Rigidbody2D>().bodyType =  RigidbodyType2D.Kinematic;
            stateInput.crushroomManager.StartWaitOnGround();
        }
        if (stateInput.crushroomManager.IsDoneWaiting()) {
            stateInput.crushroomManager.SetWaitStatus(false);
            character.ChangeState<CrushroomRiseUpState>();
        }
    }
}
