using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// State triggers when unit has a move command but no target

public class UnitWalkState : UnitStateBase
{

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (controller.unit.currentTarget || controller.unit.isAttackingGround)
        {
            unitFSM.SetBool(Constants.STATE_HASTARGET, true);
        }

        //if (controller.unit.RemainingDistance() <= 1.0f) { 
        //    unitFSM.SetBool(Constants.STATE_ISMOVING, false);
        //}       
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        //controller.unit.moveEvents.OnMoveReached?.Invoke(controller.unit.transform.position);

    }
}
