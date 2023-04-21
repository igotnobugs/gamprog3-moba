using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Barracks : Structure
{
    public BarracksType barracksType;

    public UnityEvent<Lane, Faction, BarracksType> OnBarracksDestroy = new UnityEvent<Lane, Faction, BarracksType>();

    protected override void OnUnitDeath()
    {
        base.OnUnitDeath();
        OnBarracksDestroy.Invoke(lane, faction, barracksType);
    }
}

public enum BarracksType
{
    Melee, Ranged
}
