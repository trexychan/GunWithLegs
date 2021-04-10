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
        RaycastHit2D hit = Physics2D.Raycast(stateInput.gameobj.transform.position, -stateInput.gameobj.transform.up, 20f, ~stateInput.crushroom.layermask);

        // stateInput.enemy_controller.TurnToFacePlayer(stateInput.player.transform.position);
        if (hit) {
            if (hit.collider.gameObject.layer == 8)
            character.ChangeState<CrushroomFallDownState>();
        }
    }
}