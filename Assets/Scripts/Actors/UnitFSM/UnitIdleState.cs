using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitIdleState : UnitStateBase
{

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        //Listens to the controller if a destination has been set
        //controller.controllerEvents.OnDestinationSet.AddListener(Mover_OnDestinationSet);

        controller.OnIdleStart();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        WaitForATarget();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        //controller.controllerEvents.OnDestinationSet.RemoveListener(Mover_OnDestinationSet);
    }




    protected virtual void Mover_OnDestinationSet(Vector3 destination)
    {
        // Sets the FSM to be moving
        unitFSM.SetBool(Constants.STATE_ISMOVING, true);
    }

    protected virtual void WaitForATarget()
    {
        // Sets the FSM to be on chase/attack depending on the booleans set here
        if (controller.unit.isAttackingGround)
        {
            unitFSM.SetBool(Constants.STATE_HASTARGET, true);
        } else
        {
            unitFSM.SetBool(Constants.STATE_HASTARGET, controller.unit.currentTarget);
        }
        
        unitFSM.SetBool(Constants.STATE_INRANGE, controller.unit.IsTargetInRange());
    }
}
