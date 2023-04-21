using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// State triggers when unit has a target but its not in its range.

public class UnitChaseState : UnitStateBase
{

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        controller.controllerEvents.OnDestinationSet.AddListener(Mover_OnDestinationSet);

        
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (!controller.unit.currentTarget && !controller.unit.isAttackingGround)
        {
            //Target died or something
            unitFSM.SetBool(Constants.STATE_INRANGE, false);
            unitFSM.SetBool(Constants.STATE_HASTARGET, false);
            return;
        }

        if (controller.unit.currentTarget)
        {
            controller.unit.JustMove(controller.unit.currentTarget.transform.position);
        } else
        {
            controller.unit.JustMove(controller.unit.currentTargetPosition);
        }

        CheckTargetDistance();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        controller.controllerEvents.OnDestinationSet.RemoveListener(Mover_OnDestinationSet);
    }


    protected virtual void Mover_OnDestinationSet(Vector3 destination)
    {     
        unitFSM.SetBool(Constants.STATE_ISMOVING, true);
    }

    protected void CheckTargetDistance()
    {
        unitFSM.SetBool(Constants.STATE_INRANGE, controller.unit.IsTargetInRange());
    }
}
