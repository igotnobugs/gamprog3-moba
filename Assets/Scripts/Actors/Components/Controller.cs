using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Moving

public class Controller : MonoBehaviour
{
    [Header("Unit")]
    [SerializeField, Range(1, 10), Tooltip("Minimum angle before continuing")]
    private float _turnAngleMinimum = 1;

    private Coroutine _coroutine_lookRotator;
    public bool hasAttacked { set; get; } = false;
    public Action OnAttackEndAction { set; get; }
    public ControllerEvents controllerEvents { private set; get; } = new ControllerEvents();

    public Unit unit { private set; get; }
    protected Animator animator { set; get; }


    protected virtual void Awake()
    {
        unit = GetComponent<Unit>();

        animator = GetComponent<Animator>();
        if (!animator)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    protected virtual void OnEnable()
    {
        unit.stateEvents.OnTakeDamage.AddListener(OnHit);
        unit.moveEvents.OnMoveReached.AddListener(OnDestinationReached);
        Ancient.OnVictory.AddListener(StopAttack);
        unit.stateEvents.OnUnitDeath.AddListener(UnitDeath);
    }

    protected virtual void OnDisable()
    {
        unit.stateEvents.OnTakeDamage.RemoveListener(OnHit);
        unit.moveEvents.OnMoveReached.RemoveListener(OnDestinationReached);
        unit.stateEvents.OnUnitDeath.RemoveListener(UnitDeath);
        Ancient.OnVictory.RemoveListener(StopAttack);
    }

    protected virtual void Update()
    {
        if (unit.RemainingDistance() <= 1.0f)
        {
            animator.SetBool(Constants.STATE_ISMOVING, false);
        }
        //Stun control
        if (unit.data.stunState > 0)
        {
            animator.SetBool(Constants.STATE_ISSTUNNED, true);
        } 
    }

    #region Overrideable
    public virtual void OnIdleStart()
    {

    }
    protected virtual void OnDestinationReached(Vector3 location)
    {
        //animator.SetBool(Constants.STATE_ISMOVING, false);
    }

    protected virtual void OnHit(float amount)
    {
        animator.SetFloat(Constants.STATE_STAGGER, 1.0f);
    }

    protected virtual void OnTargetDeath(Unit targetUnit, Unit instigator)
    {    
        targetUnit.stateEvents.OnUnitDeath.RemoveListener(OnTargetDeath);
        unit.SetTarget(null);
    }
    #endregion

    #region General
    public virtual void UnitDeath(Unit deadUnit, Unit killer)
    {
        CancelAction();
    }

    // Stops any action
    public virtual void CancelAction()
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
        float turnSpeed = (360.0f / unit.data.turnRateSpeed);
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
        if (!unit.isAlive) return;

        if (hasAttacked)
        {
            OnAttackEndAction = command;
        } else
        {
            command?.Invoke();
        }
    }
    #endregion

    #region Moving
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
    public void StopAttacking()
    {
        unit.StopMoving();
        unit.SetTarget(null);
    }

    private void StopAttack()
    {
        unit.SetTarget(null);
    }

    public virtual bool AttackTarget(Unit targetUnit, Skill skill = null)
    {
        if (!targetUnit) return false;

        this.RestartCoroutine(
            LookBeforeAction(targetUnit.transform.position, () => {
                if (unit.Attack(targetUnit, skill))
                {
                    unit.SetFollow(targetUnit, unit.data.attackRange.GetTotal());
                    targetUnit.stateEvents.OnUnitDeath.AddListener(OnTargetDeath);
                    controllerEvents.OnTargetSet?.Invoke(targetUnit.transform.position);
                }
            }
            ), ref _coroutine_lookRotator);

        return true;
    }

    public virtual bool AttackTarget(Vector3 targetPosition, Skill skill = null)
    {
        this.RestartCoroutine(
            LookBeforeAction(targetPosition, () => {
                if (unit.Attack(targetPosition, skill))
                {
                    //unit.SetFollow(targetUnit, unit.stats.attackRange);
                    //targetUnit.stateEvents.OnUnitDeath.AddListener(OnTargetDeath);
                    controllerEvents.OnTargetSet?.Invoke(targetPosition);
                }
            }
            ), ref _coroutine_lookRotator);

        return true;
    }
    #endregion

}

[System.Serializable]
public class ControllerEvents
{
    public UnityEvent<Vector3> OnDestinationSet = new UnityEvent<Vector3>();
    public UnityEvent<Vector3> OnDestinationReached = new UnityEvent<Vector3>();
    public UnityEvent<Vector3> OnTargetSet = new UnityEvent<Vector3>();
}