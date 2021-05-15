using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MajorRunState : MajorState
{
    public override void Enter(MajorStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        base.Enter(stateInput, transitionInfo);
        stateInput.anim.Play("major_run");
        if (stateInput.enemyController.facingRight)
        {
            stateInput.rb.velocity = new Vector2(stateInput.major_inconvenience.moveSpeed, 0);
        } else
        {
            stateInput.rb.velocity = new Vector2(-stateInput.major_inconvenience.moveSpeed, 0);
        }
        
    }

    public override void Update(MajorStateInput stateInput)
    {
        Vector2 checkpoint1;
        RaycastHit2D hit1;
        if (!stateInput.enemyController.facingRight)
        {
            checkpoint1 = new Vector2(stateInput.collider.bounds.center.x - stateInput.collider.bounds.extents.x , stateInput.collider.bounds.center.y);
            hit1 = Physics2D.Raycast(checkpoint1, Vector2.left, 1f, 1 << 9);
            Debug.DrawRay(checkpoint1, Vector2.down, Color.blue, 1f);
        } else {
            checkpoint1 = new Vector2(stateInput.collider.bounds.center.x + stateInput.collider.bounds.extents.x , stateInput.collider.bounds.center.y);
            hit1 = Physics2D.Raycast(checkpoint1, Vector2.right, 1f, 1 << 9);
            Debug.DrawRay(checkpoint1, Vector2.down, Color.blue, 1f);
        }
        
        if (hit1)
        {
            stateInput.enemyController.StopMoving();
            character.ChangeState<MajorIdleState>();
        }
        
    }

    
}
