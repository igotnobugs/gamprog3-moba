using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Creep Spawner Data")]
public class CreepSpawnerData : ScriptableObject
{
    [BoxGroup("Settings")]
    public float spawnInterval = 0.25f;

    [BoxGroup("Settings")]
    public float siegeWaveInterval = 1.0f;

    [BoxGroup("Settings")]
    public float meleePerWave = 1.0f;

    [BoxGroup("Settings")]
    public float rangePerWave = 1.0f;

    [BoxGroup("Settings")]
    public float siegePerWave = 1.0f;


    [BoxGroup("Base Creeps")]
    public Creep meleeCreep;

    [BoxGroup("Base Creeps")]
    public Creep rangeCreep;

    [BoxGroup("Base Creeps")]
    public Creep siegeCreep;

    [BoxGroup("Super Creeps")]
    public Creep superMeleeCreep;

    [BoxGroup("Super Creeps")]
    public Creep superRangeCreep;

    [BoxGroup("Super Creeps")]
    public Creep superSiegeCreep;

    [BoxGroup("Base Creep Data")]
    public UnitData baseMeleeCreep;

    [BoxGroup("Base Creep Data")]
    public UnitData baseSuperMeleeCreep;

    [BoxGroup("Base Creep Data")]
    public UnitData baseRangedCreep;

    [BoxGroup("Base Creep Data")]
    public UnitData baseSuperRangedCreep;
}
