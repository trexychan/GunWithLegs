using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MajorJumpState : MajorState
{
    private float jumpHeight = 5f;
    private bool isGrounded;
    public override void Enter(MajorStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        base.Enter(stateInput, transitionInfo);
        stateInput.anim.Play("major_jump");
    }
    public override void FixedUpdate(MajorStateInput stateInput)
    {
        isGrounded = stateInput.enemyController.checkIsGrounded();
        if (isGrounded && stateInput.major_inconvenience.hasJumped && stateInput.rb.velocity.y < 0)
        {
            stateInput.enemyController.DropHealth(2);
            character.ChangeState<MajorIdleState>();
            return;
        }
    }

    // public void JumpAttack()
    // {
    //     hasJumped = true;
    //     float distanceFromPlayer = player.transform.position.x - gameobj.transform.position.x;
    //     if (isGrounded)
    //     {
    //         Debug.Log("jumped");
    //         // stateInput.rb.bodyType = RigidbodyType2D.Dynamic;
    //         rb.AddForce(new Vector2(distanceFromPlayer, jumpHeight), ForceMode2D.Impulse);
    //     }
    // }

}
