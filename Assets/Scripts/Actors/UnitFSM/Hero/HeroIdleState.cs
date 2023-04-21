using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Inherits from UnitIdleState
// Completely the same only that it spawns a moveindicator when issued a move command
// State is set when not moving and no target


public class HeroIdleState : UnitIdleState
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        controller.unit.data.OnModifyAtkSpeed += OnChangeAttackSpeed;
        //unitFSM.SetFloat(Constants.STATE_ATKSPDMULTIPLIER, controller.unit.data.GetAtkSpdMultiplier());
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        unitFSM.SetBool(Constants.STATE_HASMANA, controller.unit.HasEnoughMana());
        
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        //controller.unit.data.OnModifyAtkSpeed -= OnChangeAttackSpeed;
    }

    public void OnChangeAttackSpeed(float speedMultiplier)
    {
        
    }
}
