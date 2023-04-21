using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Base of All States
// Inheritance Level O_O
// UnitStateBase -> Unit(STATES)State -> Hero(STATES)State

public class UnitStateBase : StateMachineBehaviour
{
    protected Animator unitFSM;
    protected Controller controller;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        unitFSM = animator;
        controller = animator.GetComponentInParent<Controller>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        // Reduces the hit stagger effect, typically from being attacked
        if (animator.GetFloat(Constants.STATE_STAGGER) > 0)
        {
            float newValue = animator.GetFloat(Constants.STATE_STAGGER) - Time.deltaTime;
            animator.SetFloat(Constants.STATE_STAGGER, newValue);
        }

        controller.unit.SetMoveSpeed(controller.unit.data.moveSpeed.GetTotal());

        //Updating Data values
        controller.unit.data.UpdateTotalAttackSpeed();

        unitFSM.SetFloat(Constants.STATE_ATKSPDMULTIPLIER, 
            controller.unit.data.baseAttackTime / controller.unit.data.totalAttackTime);

    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
/*
[SharedBetweenAnimators]
public class UnitStateSetUpBase : StateMachineBehaviour
{

}*/