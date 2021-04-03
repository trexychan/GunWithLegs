using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostWanderState : GhostState
{
    //float timer = 0f;
    // Start is called before the first frame update
    public override void Enter(GhostStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        //base.Enter(stateInput, transitionInfo);
        stateInput.anim.Play("Enemy_Move");
        stateInput.rb.velocity = new Vector2(-stateInput.ghost.maxSpeed, 0f);
    }

    // Update is called once per frame
    public override void Update(GhostStateInput stateInput)
    {
        if (stateInput.enemy_controller.spottedPlayer())
        {
            stateInput.enemy_controller.TurnToFacePlayer(stateInput.player.transform.position);
            stateInput.anim.Play("Enemy_Attack");

            stateInput.gameobj.transform.position = Vector2.MoveTowards(stateInput.gameobj.transform.position,
            stateInput.player.transform.position, stateInput.ghost.maxSpeed * Time.deltaTime);
            
        } else {
            if (stateInput.gameobj.transform.position.x > stateInput.ghost.waypoint_r.position.x) 
            {
                stateInput.gameobj.transform.position = stateInput.ghost.waypoint_r.position;
                stateInput.enemy_controller.facingRight = !stateInput.enemy_controller.facingRight;
                stateInput.gameobj.transform.rotation = Quaternion.Euler(0,0,0);
                stateInput.rb.velocity = -stateInput.rb.velocity;
            }
            if (stateInput.gameobj.transform.position.x < stateInput.ghost.waypoint_l.position.x)
            {
                stateInput.gameobj.transform.position = stateInput.ghost.waypoint_l.position;
                stateInput.enemy_controller.facingRight = !stateInput.enemy_controller.facingRight;
                stateInput.gameobj.transform.rotation = Quaternion.Euler(0,180,0);
                stateInput.rb.velocity = -stateInput.rb.velocity;
            }
        }
    }
}
