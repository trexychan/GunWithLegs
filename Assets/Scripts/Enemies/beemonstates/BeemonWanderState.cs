using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeemonWanderState : BeemonState
{
    public override void Enter(BeemonStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        stateInput.anim.Play("Enemy_Move");
        stateInput.rb.velocity = new Vector2(-stateInput.beemon.maxSpeed, 0f);
    }

    public override void Update(BeemonStateInput stateInput)
    {
        if (stateInput.gameobj.transform.position.x > stateInput.beemon.waypoint_r.position.x)
        {
            stateInput.gameobj.transform.position = stateInput.beemon.waypoint_r.position;
            stateInput.enemy_controller.facingRight = !stateInput.enemy_controller.facingRight;
            stateInput.gameobj.transform.rotation = Quaternion.Euler(0,0,0);
            stateInput.rb.velocity = -stateInput.rb.velocity;
        }
        if (stateInput.gameobj.transform.position.x < stateInput.beemon.waypoint_l.position.x)
        {
            stateInput.gameobj.transform.position = stateInput.beemon.waypoint_l.position;
            stateInput.enemy_controller.facingRight = !stateInput.enemy_controller.facingRight;
            stateInput.gameobj.transform.rotation = Quaternion.Euler(0,180,0);
            stateInput.rb.velocity = -stateInput.rb.velocity;
        }
    }
}
