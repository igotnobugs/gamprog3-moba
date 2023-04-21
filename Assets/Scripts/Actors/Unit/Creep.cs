using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Creep : Unit
{
    public static List<Creep> spawnedCreepList = new List<Creep>();

    protected virtual void Start()
    {
        spawnedCreepList.Add(this);
    }

    protected override void OnUnitDeath()
    {
        spawnedCreepList.Remove(this);

        base.OnUnitDeath();
    }
}
