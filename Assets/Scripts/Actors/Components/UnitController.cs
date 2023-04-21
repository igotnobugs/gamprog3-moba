using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.AI;


// Attacking
/*
public abstract class UnitController : Controller
{
    [Header("Unit")]
    [SerializeField, Range(1, 10), Tooltip("Minimum angle before continuing")]
    private float _turnAngleMinimum = 1;

    private Coroutine _coroutine_lookRotator;
    public bool hasAttacked { set; get; } = false;
    public Action OnAttackEndAction { set; get; }
    public ControllerEvents controllerEvents { private set; get; } = new ControllerEvents();

    protected override void OnEnable()
    {
        base.OnEnable();
        unit.moveEvents.OnMoveReached.AddListener(OnDestinationReached);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        unit.moveEvents.OnMoveReached.RemoveListener(OnDestinationReached);
    }

    #region Overrideable
    protected virtual void OnDestinationReached(Vector3 location)
    {       
        //animator.SetBool(Constants.STATE_ISMOVING, false);
    }
    #endregion

    #region General
    // Stops any action
    protected virtual void CancelAction()
    {
        unit.StopMoving();
        this.TryStopCoroutine(ref _coroutine_lookRotator);       
    }

    private IEnumerator LookBeforeAction(Vector3 destination, Action OnFinished = null)
    {
        unit.PauseMove();

        Vector3 posSameHeight = new Vector3(destination.x, transform.position.y, destination.z);
        Quaternion targetRotation = Quaternion.LookRotation(posSameHeight - transform.position, Vector3.up);

        float time = 0.0f;
        float turnSpeed = (360.0f / unit.stats.turnRateSpeed);
        while (Quaternion.Angle(transform.rotation, targetRotation) > _turnAngleMinimum)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, (time / turnSpeed));
            time += Time.deltaTime;
            yield return null;
        }

        unit.ResumeMove();
        OnFinished?.Invoke();
        yield return null;
    }

    public void AttemptCommand(Action command)
    {
        if (hasAttacked)
        {
            OnAttackEndAction = command;
        } else
        {
            command?.Invoke();
        }
    }
    #endregion

    #region Walk
    // For Move Commands only, it set the target to null, Attack Commmands uses Chase
    public void MoveToPosition(Vector3 position, float stopDistance = 0)
    {
        this.RestartCoroutine(
            LookBeforeAction(position, () => {
                animator.SetBool(Constants.STATE_ISMOVING, true);
                unit.Move(position, stopDistance);          
                controllerEvents.OnDestinationSet?.Invoke(position);
            }
            ), ref _coroutine_lookRotator);
    }

    #endregion

    #region Attacking
    protected override bool AttackTarget(Unit targetUnit, Skill skill = null)
    {
        this.TryStopCoroutine(ref _coroutine_lookRotator);

        unit.StopMoving();

        if (!targetUnit)
        {
            return base.AttackTarget(targetUnit, skill);
        }

        this.RestartCoroutine(
            LookBeforeAction(targetUnit.transform.position, () => {

                base.AttackTarget(targetUnit, skill);

                unit.SetFollow(targetUnit, unit.stats.attackRange);
                controllerEvents.OnTargetSet?.Invoke(targetUnit.transform.position);
            }
            ), ref _coroutine_lookRotator);

        return true;
    }
    #endregion
}
*/

/*
[System.Serializable]
public class ControllerEvents
{
    public UnityEvent<Vector3> OnDestinationSet = new UnityEvent<Vector3>();
    public UnityEvent<Vector3> OnDestinationReached = new UnityEvent<Vector3>();
    public UnityEvent<Vector3> OnTargetSet = new UnityEvent<Vector3>();
}*/