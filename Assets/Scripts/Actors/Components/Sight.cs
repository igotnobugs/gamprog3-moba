using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Must only collide with layer 7 or the Unit Layer

public class Sight : MonoBehaviour
{
    public float sightRange = 750.0f;

    [SerializeField]
    private List<GameObject> _validEnemyTargets = new List<GameObject>(); 

    [SerializeField]
    private List<GameObject> _validAllyTargets = new List<GameObject>();

    public List<GameObject> ValidEnemyTargets { get { return _validEnemyTargets; } }
    public List<GameObject> ValidAllyTargets { get { return _validAllyTargets; } }

    public Unit unitOwner;
    public SphereCollider sightCollider;

    public bool GetStatsFromOwner = false;

    public TargetingEvents targetingEvents = new TargetingEvents();

    private void Awake()
    {
        unitOwner = GetComponentInParent<Unit>();
        sightCollider = GetComponent<SphereCollider>();
        GetStatsFromOwner = true;
    }

    private void Start()
    {
        if(GetStatsFromOwner)
        {
            sightRange = unitOwner.data.sightRange;
            sightCollider.radius = sightRange.ToUnitValue();
        }
    }

    #region TargetDetection
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Unit enteredUnit))
        {
            if (CheckIfNewTargetIsValidEnemy(enteredUnit))
            {
                _validEnemyTargets.Add(enteredUnit.gameObject);
                targetingEvents.OnTargetFound?.Invoke(enteredUnit);
            }
            else if(CheckIfNewTargetIsValidAlly(enteredUnit))
            {
                _validAllyTargets.Add(enteredUnit.gameObject);
                targetingEvents.OnAllyFound?.Invoke(enteredUnit);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Unit exitedUnit))
        {
            bool removeSuccess = true;
            if (CheckIfTargetIsValidEnemy(exitedUnit))
            {
                removeSuccess = _validEnemyTargets.Remove(other.gameObject);
                if (!removeSuccess) return;
                targetingEvents.OnTargetExit?.Invoke(exitedUnit);

                if (unitOwner.currentTarget != exitedUnit) return;
                SortTargetsByDistance(_validEnemyTargets);
            }
            else
            {
                removeSuccess = _validAllyTargets.Remove(other.gameObject);
                if (!removeSuccess) return;
                targetingEvents.OnAllyExit?.Invoke(exitedUnit);

                if (unitOwner.currentTarget != exitedUnit) return;
                SortTargetsByDistance(_validAllyTargets);
            }  
        }
    }

    private bool CheckIfNewTargetIsValidEnemy(Unit unitFound)
    {
        if (unitFound.faction == unitOwner.faction) return false;
        if (_validEnemyTargets.Contains(unitFound.gameObject)) return false;
        if (unitFound.isInvulnerable) return false;
        return true;
    }

    private bool CheckIfNewTargetIsValidAlly(Unit unitFound)
    {
        if (unitFound == unitOwner) return false;
        if (unitFound.faction != unitOwner.faction) return false;
        if (_validAllyTargets.Contains(unitFound.gameObject)) return false;
        //if (unitFound.isInvulnerable) return false;
        return true;
    }

    private bool CheckIfTargetIsValidEnemy(Unit unitFound)
    {
        if (unitFound.faction == unitOwner.faction) return false;
        return true;
    }
    #endregion

    public Unit GetNewEnemyTarget()
    {
        _validEnemyTargets.RemoveAll(x => x == null);

        if (_validEnemyTargets.Count <= 0) return null;

        SortTargetsByDistance(_validEnemyTargets);     
        return _validEnemyTargets[0].GetComponent<Unit>();
    }

    public Unit GetNewAllyTarget()
    {
        _validAllyTargets.RemoveAll(x => x == null);

        if (_validAllyTargets.Count <= 0) return null;

        SortTargetsByDistance(_validAllyTargets);
        return _validAllyTargets[0].GetComponent<Unit>();
    }

    public bool RemoveAsEnemyTarget(GameObject gameObject)
    {
        return _validEnemyTargets.Remove(gameObject);
    }

    public bool RemoveAsAllyTarget(GameObject gameObject)
    {
        return _validAllyTargets.Remove(gameObject);
    }

    private void SortTargetsByDistance(List<GameObject> targetList)
    {
        if (_validEnemyTargets.Count <= 0) return;
        _validEnemyTargets.Sort((a, b) =>
            GetDistanceFromOwner(a).CompareTo(GetDistanceFromOwner(b)
        ));
    }

    private float GetDistanceFromOwner(GameObject unit)
    {
        return ((Vector3.Distance(unit.transform.position, unitOwner.transform.position)
            - unit.GetComponent<Unit>().width.ToUnitValue()));
    }
}

public class TargetingEvents
{
    public UnityEvent<Unit> OnTargetFound = new UnityEvent<Unit>();
    public UnityEvent<Unit> OnTargetExit = new UnityEvent<Unit>();

    public UnityEvent<Unit> OnAllyFound = new UnityEvent<Unit>();
    public UnityEvent<Unit> OnAllyExit = new UnityEvent<Unit>();
}