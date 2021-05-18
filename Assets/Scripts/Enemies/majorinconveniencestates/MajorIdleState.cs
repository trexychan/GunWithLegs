using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MajorIdleState : MajorState
{
    public override void Enter(MajorStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        base.Enter(stateInput, transitionInfo);
        stateInput.anim.Play("major_idle");
        stateInput.enemyController.EnemyPause(1f);
    }

    public override void Update(MajorStateInput stateInput)
    {
        stateInput.enemyController.TurnToFacePlayer(stateInput.player.transform.position);
        if (stateInput.enemyController.canAct)
        {
            if (stateInput.enemyController.currentHealth > ((int)stateInput.enemyController.maxHealth/2))
            {
                character.ChangeState<MajorRunState>();
                return;
            } else if (stateInput.enemyController.currentHealth <= ((int)stateInput.enemyController.maxHealth/2))
            {
                stateInput.major_inconvenience.moveSpeed = 6f;
                stateInput.major_inconvenience.hasJumped = false;
                int random_num = (int)Random.Range(0, 2);
                if (random_num == 0)
                {
                    character.ChangeState<MajorRunState>();
                    return;
                } else
                {
                    character.ChangeState<MajorJumpState>();
                    return;
                }
                
            }
        }   
    }
}
