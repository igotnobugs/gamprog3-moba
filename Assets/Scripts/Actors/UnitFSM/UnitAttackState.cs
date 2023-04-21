using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Also plays the attack animation
// The Attack animation MUST have frame events embedded that calls on to the Controller

public class UnitAttackState : UnitStateBase
{
    [SerializeField, Tooltip("Once the attack animation starts it will hit.")]
    protected bool _attackSureHit = true;

    [SerializeField, Tooltip("Allows the character to move after attacking.")]
    protected bool _allowAnimationCancel = true;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        controller.controllerEvents.OnDestinationSet.AddListener(Mover_OnDestinationSet);

        controller.unit.attackEvents.OnAttackStart.AddListener(OnAttackStart);
        controller.unit.attackEvents.OnAttackHitFrame.AddListener(OnAttackHitFrame);
        controller.unit.attackEvents.OnAttackEnd.AddListener(OnAttackEnd);

        unitFSM.SetBool(Constants.STATE_ISMOVING, false);

        controller.unit.StopMoving();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (!controller.unit.currentTarget && !controller.unit.isAttackingGround)
        {
            if (controller.hasAttacked) return;


            unitFSM.SetBool(Constants.STATE_INRANGE, false);
            unitFSM.SetBool(Constants.STATE_HASTARGET, false);
            return;
        }

        if (!controller.unit.IsTargetInRange() && !_attackSureHit)
        {
            unitFSM.SetBool(Constants.STATE_INRANGE, false);
        } 
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        controller.controllerEvents.OnDestinationSet.RemoveListener(Mover_OnDestinationSet);

        controller.unit.attackEvents.OnAttackStart.RemoveListener(OnAttackStart);
        controller.unit.attackEvents.OnAttackHitFrame.RemoveListener(OnAttackHitFrame);
        controller.unit.attackEvents.OnAttackEnd.RemoveListener(OnAttackEnd);

        controller.unit.ResumeMove();
    }


    private void OnAttackStart()
    {

    }

    private void OnAttackHitFrame()
    {
        if (_allowAnimationCancel) return;

        controller.hasAttacked = true;
    }

    private void OnAttackEnd()
    {
        controller.hasAttacked = false;

        // Check if unit does not have enough mana
        if(!controller.unit.HasEnoughMana())
        {
            unitFSM.SetBool(Constants.STATE_HASMANA, false);
        }

        //Check if the target is dead or is unset 
        if (!controller.unit.IsTargetInRange())
        {
            unitFSM.SetBool(Constants.STATE_INRANGE, false);
        }

        controller.OnAttackEndAction?.Invoke();
        controller.OnAttackEndAction = null;
    }

    private void Mover_OnDestinationSet(Vector3 destination)
    {
        unitFSM.SetBool(Constants.STATE_ISMOVING, true);
    }

    private void OnAnimationAttackStart()
    {

    }
}
