using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public abstract class Character : Actor
{  
    private Actor _followTarget;
    private NavMeshAgent _agent;

    public MoveEvents moveEvents { private set; get; } = new MoveEvents();

    private bool canMove = true;

    protected override void Awake()
    {
        base.Awake();

        _agent = GetComponent<NavMeshAgent>();

        if (!_agent)
            canMove = false;
    }

    private void OnEnable()
    {
        Ancient.OnVictory.AddListener(PauseMove);
    }
    private void OnDisable()
    {
        Ancient.OnVictory.RemoveListener(PauseMove);
    }

    /// <summary>
    /// Automatically converts to Unit Values
    /// </summary>
    /// <param name="moveSpeed"></param>
    public void SetMoveSpeed(float moveSpeed)
    {
        if (!_agent) return;
        _agent.speed = moveSpeed.ToUnitValue();
    }

    #region Movement
    public void StopMoving()
    {
        if (!_agent) return;
        _agent.isStopped = true;
        _agent.destination = transform.position;
    }
    #endregion

    #region NavMeshMover
    public void Warp(Vector3 warpDestination)
    {
        _agent.Warp(warpDestination);
    }

    public void PauseMove()
    {
        if (!canMove) return;
        _agent.isStopped = true;
    }

    public void ResumeMove()
    {
        if (!canMove) return;
        _agent.isStopped = false;
    }

    public void Move(Vector3 location, float stopDistance = 0)
    {
        _agent.destination = location;
        _agent.stoppingDistance = stopDistance;

        _agent.isStopped = false;

        moveEvents.OnMoveSet?.Invoke(location);
    }

    public void JustMove(Vector3 location)
    {
        //if (!_followTarget) return;
        
        _agent.destination = location;
    }

    public void SetFollow(Unit targetActor, float range = 0)
    {
        if (!targetActor) return;
        if (!canMove) return;

        _agent.isStopped = false;
        _followTarget = targetActor;     
        _agent.stoppingDistance = targetActor.width.ToUnitValue() + range.ToUnitValue();     
    }

    public void StopFollowing()
    {
        _followTarget = null;
        StopMoving();
    }

    public float RemainingDistance()
    {
        if (!canMove) return 0;
        return _agent.remainingDistance;
    }
    #endregion

    private void OnDrawGizmos()
    {
        if (!_agent) return;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(
            transform.position,
            _agent.destination
            );
    }
}

[System.Serializable]
public class MoveEvents
{
    public UnityEvent<Vector3> OnMoveSet = new UnityEvent<Vector3>();
    public UnityEvent<Vector3> OnMoveReached = new UnityEvent<Vector3>();
    public UnityEvent<Vector3> OnMoveStopped = new UnityEvent<Vector3>();
}

