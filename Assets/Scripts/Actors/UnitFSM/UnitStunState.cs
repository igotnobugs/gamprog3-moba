using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStunState : UnitStateBase
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        controller.unit.StopMoving();
        controller.unit.SetMoveSpeed(0);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (controller.unit.data.stunState > 0)
        {
            controller.unit.data.stunState -= Time.deltaTime;
        } else
        {
            animator.SetBool(Constants.STATE_ISSTUNNED, false);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        controller.unit.SetMoveSpeed(controller.unit.data.moveSpeed.GetTotal());
    }

}
