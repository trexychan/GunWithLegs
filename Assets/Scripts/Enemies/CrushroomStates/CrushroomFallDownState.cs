using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushroomFallDownState : CrushroomState
{
    bool falling;
    
    public override void Enter(CrushroomStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        stateInput.crushroomManager = stateInput.crushroom.GetComponent<CrushroomManager>();
        stateInput.rb.bodyType =  RigidbodyType2D.Dynamic;
        stateInput.rb.gravityScale =  2f;
        stateInput.rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        stateInput.anim.Play("crushroom_attack");
        falling = true;
    }

    public override void Update(CrushroomStateInput stateInput)
    {
        
        // stateInput.anim.SetFloat("speed", stateInput.rb.velocity.y);
        if (stateInput.rb.IsTouchingLayers(1 << 9) && falling) { // if the crushroom hits the ground
            Debug.Log("Landed!");
            CamController.Instance.Shake(5, 0.3f);
            stateInput.anim.Play("crushroom_land");
            falling = false;
            stateInput.rb.bodyType =  RigidbodyType2D.Static;
            stateInput.crushroomManager.StartWaitOnGround();
            
        }
        if (stateInput.crushroomManager.IsDoneWaiting()) {
            stateInput.crushroomManager.SetWaitStatus(false);
            character.ChangeState<CrushroomRiseUpState>();
        }
    }
}
