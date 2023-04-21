using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepController : Controller
{
    [Header("Creep")]
    public List<Waypoint> laneWaypoints = new List<Waypoint>();

    private int currentWpIndex = 0;
    private Sight targeting;

    public bool loopWaypoints = false;

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

    protected virtual void Start()
    {
        GoToWaypoint();
    }

    //When creep starts idling
    public override void OnIdleStart()
    {
        base.OnIdleStart();
    }

    protected override void OnTargetDeath(Unit targetUnit, Unit instigator)
    {
        base.OnTargetDeath(targetUnit, instigator);

        targeting.RemoveAsEnemyTarget(targetUnit.gameObject);

        Unit newTarget = targeting.GetNewEnemyTarget();

        if (!newTarget)
        {
            GoToWaypoint();
        } else
        {
            Attack(newTarget);
        }   
    }

    #region MoveToWaypoints
    private void GoToNextWaypoint()
    {
        currentWpIndex++;
        if (currentWpIndex >= laneWaypoints.Count)
        {
            if (!loopWaypoints) return;

            currentWpIndex = 0;
        }

        GoToWaypoint();
    }

    private void GoToWaypoint()
    {
        AttemptCommand(() =>
        {
            if (laneWaypoints.Count <= 0) return;
            MoveToPosition(laneWaypoints[currentWpIndex].groundPosition, 1.0f);

        });
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Waypoint"))
        {
            if (other.TryGetComponent(out Waypoint waypoint))
            {
                if (laneWaypoints.Contains(waypoint))
                {
                    GoToNextWaypoint();
                }
            }
        }
    }
    #endregion

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
            //unit.SetTarget(targeting.GetNewTarget());
            Unit newTarget = targeting.GetNewEnemyTarget();

            if (!newTarget)
            {
                GoToWaypoint();
            } else
            {
                Attack(newTarget);
            }
        }

    }
    #endregion

    private void OnDrawGizmos()
    {
        if (currentWpIndex < 0 || currentWpIndex >= laneWaypoints.Count) return;
        if (laneWaypoints.Count <= 0) return;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(
            transform.position,
            laneWaypoints[currentWpIndex].transform.position
            );
    }
}
