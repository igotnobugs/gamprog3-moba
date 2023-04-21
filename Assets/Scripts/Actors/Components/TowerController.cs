using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : Controller
{
    [Header("Tower")]
    private Sight targeting;


    protected override void Awake()
    {
        base.Awake();

        targeting = GetComponentInChildren<Sight>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        targeting.targetingEvents.OnTargetFound.AddListener(Attack);
        targeting.targetingEvents.OnTargetExit.AddListener(RemoveChaseTarget);
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        targeting.targetingEvents.OnTargetFound.RemoveListener(Attack);
        targeting.targetingEvents.OnTargetExit.RemoveListener(RemoveChaseTarget);
    }

    protected override void OnTargetDeath(Unit targetUnit, Unit instigator)
    {
        base.OnTargetDeath(targetUnit, instigator);

        targeting.RemoveAsEnemyTarget(targetUnit.gameObject);

        Unit newTarget = targeting.GetNewEnemyTarget();

        if (newTarget)
        {
            Attack(newTarget);
        } else
        {
            unit.SetTarget(null);
        }
    }

    #region Attacking
    private void Attack(Unit targetUnit)
    {
        if (unit.currentTarget) return;

        AttemptCommand(() => {
            AttackTarget(targetUnit);
        });
    }

    private void RemoveChaseTarget(Unit targetUnit)
    {
        if (unit.currentTarget == targetUnit)
        {
            Unit newTarget = targeting.GetNewEnemyTarget();
            if (newTarget)
            {
                Attack(newTarget);
            } else
            {
                unit.SetTarget(null);
            }

        }

    }
    #endregion

}
