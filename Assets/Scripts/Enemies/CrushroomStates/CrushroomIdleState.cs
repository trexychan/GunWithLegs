using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushroomIdleState : CrushroomState
{
    float width, leftX, rightX;

    public override void Enter(CrushroomStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        // base.Enter(stateInput, transitionInfo);
        // stateInput.anim.Play("Enemy_Idle");
        
        width = stateInput.crushroom.GetComponent<SpriteRenderer>().bounds.size.x;
        leftX = stateInput.crushroom.transform.position.x - (width/2);
        rightX = stateInput.crushroom.transform.position.x + (width/2);
    }

    public override void Update(CrushroomStateInput stateInput)
    {
        // stateInput.enemy_controller.TurnToFacePlayer(stateInput.player.transform.position);
        if (stateInput.player.transform.position.x >= leftX && stateInput.player.transform.position.x <= rightX) {
            character.ChangeState<CrushroomFallDownState>();
        }
    }
}