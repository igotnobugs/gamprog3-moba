using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using NaughtyAttributes;
using System;

public class HeroController : Controller
{
    [Header("Hero")]
    [SerializeField]
    protected GameObject moveIndicatorPrefab;

    [SerializeField]
    protected GameObject attackIndicatorPrefab;

    protected GameObject actionIndicatorSpawned;

    [Header("AI")]
    [SerializeField]
    [Header("Creep")]
    public Lane laneToFollow;
    public Waypoint[] laneWaypoints;
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
        unit.stateEvents.OnUnitRespawn.AddListener(OnRespawn);
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        targeting.targetingEvents.OnTargetFound.RemoveListener(Attack);
        targeting.targetingEvents.OnTargetExit.RemoveListener(RemoveChaseTarget);
        unit.stateEvents.OnUnitRespawn.RemoveListener(OnRespawn);
    }

    protected virtual void Start()
    {
        if (!unit.isAI)
        {
            targeting.gameObject.SetActive(false);
        } else
        {
            SetRandomLane();
            GoToWaypoint(currentWpIndex);
        }
    }

    protected override void OnDestinationReached(Vector3 location)
    {
        base.OnDestinationReached(location);

        RemoveMoveIndicator();
    }  

    #region Indicators
    public void SpawnMoveIndicator(Vector3 locaion)
    {
        if (actionIndicatorSpawned == null)
        {
            if (moveIndicatorPrefab != null)
                actionIndicatorSpawned = Instantiate(moveIndicatorPrefab, locaion, Quaternion.identity);
        } else
        {
            actionIndicatorSpawned.GetComponent<ParticleSystem>().time = 0;
            actionIndicatorSpawned.transform.position = locaion;
        }
    }

    public void RemoveMoveIndicator()
    {
        if (actionIndicatorSpawned != null)
            Destroy(actionIndicatorSpawned);
    }
    #endregion

 
    protected override void OnTargetDeath(Unit targetUnit, Unit instigator)
    {
        base.OnTargetDeath(targetUnit, instigator);

        targeting.RemoveAsEnemyTarget(targetUnit.gameObject);

        if (!unit.isAI) return; 
        Unit newTarget = targeting.GetNewEnemyTarget();

        if (!newTarget)
        {
            GoToWaypoint(currentWpIndex);
        } else
        {
            Attack(newTarget);
        }
    }

    #region MoveToWaypoints
    private void SetRandomLane()
    {
        int laneInt = UnityEngine.Random.Range(0, 2);
        switch (laneInt)
        {
            case 0:
                laneWaypoints = WaypointManager.Instance.topWaypointSet;
                laneToFollow = Lane.Top;
                break;
            case 1:
                laneWaypoints = WaypointManager.Instance.midWaypointSet;
                laneToFollow = Lane.Middle;
                break;
            case 2:
                laneWaypoints = WaypointManager.Instance.botWaypointSet;
                laneToFollow = Lane.Bottom;
                break;
        }

        if (unit.faction == Faction.Radiant)
        {
            currentWpIndex = 0;
        } else
        {
            currentWpIndex = laneWaypoints.Length - 1;
        }
    }

    private void OnRespawn()
    {
        SetRandomLane();
        GoToWaypoint(currentWpIndex);
    }

    private void GoToNextWaypoint()
    {
        if (unit.faction == Faction.Radiant)
        {
            currentWpIndex++;
            if (currentWpIndex >= laneWaypoints.Length - 1)
            {
                if (!loopWaypoints) return;

                currentWpIndex = 0;
            }
        } else
        {
            currentWpIndex--;
            if (currentWpIndex <= 0)
            {
                if (!loopWaypoints) return;

                currentWpIndex = laneWaypoints.Length - 1;
            }
        }
        GoToWaypoint(currentWpIndex);
    }

    private void GoToWaypoint(int index)
    {
        AttemptCommand(() =>
        {
            if (laneWaypoints.Length <= 0) return;
            MoveToPosition(laneWaypoints[index].groundPosition, 1.0f);

        });
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!unit.isAI) return;

        if (other.gameObject.CompareTag("Waypoint"))
        {
            if (other.TryGetComponent(out Waypoint waypoint))
            {
               
                //if (laneWaypoints.Contains(waypoint))
                //{
                    GoToNextWaypoint();
                //}
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
                GoToWaypoint(currentWpIndex);
            } else
            {
                Attack(newTarget);
            }
        }

    }
    #endregion
}
