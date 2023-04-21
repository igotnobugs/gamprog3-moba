using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepSpawner : MonoBehaviour
{
    [SerializeField]
    private bool willSpawnCreeps = false;

    [SerializeField]
    private int currentWave = 0;

    [SerializeField]
    private CreepSpawnerData creepSpawnerData;

    [SerializeField, Tooltip("If empty, uses self as spawn point.")]
    private Transform spawnPoint;

    [SerializeField] 
    private List<Waypoint> laneWaypoints = new List<Waypoint>();

    [SerializeField]
    private Lane lane;

    [SerializeField]
    private Faction faction;

    [SerializeField]
    private Barracks opposingMeleeBarracks;

    [SerializeField]
    private Barracks opposingRangedBarracks;

    private bool willSpawnSuperMelee = false;
    private bool willSpawnSuperRanged = false;
    private bool willSpawnSuperSiege = false;

    private List<Creep> creeps = new List<Creep>();

    private void OnEnable()
    {
        opposingMeleeBarracks.stateEvents.OnUnitDeath.AddListener(SpawnSuperMelee);
        opposingRangedBarracks.stateEvents.OnUnitDeath.AddListener(SpawnSuperRanged);
    }

    private void OnDisable()
    {
        opposingMeleeBarracks.stateEvents.OnUnitDeath.RemoveListener(SpawnSuperMelee);
        opposingRangedBarracks.stateEvents.OnUnitDeath.RemoveListener(SpawnSuperRanged);
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (!spawnPoint)
        {
            spawnPoint = transform;
        } 
        PopulateCreepList();
    }

    public void SpawnCreeps(int waveCount)
    {
        currentWave = waveCount;
        willSpawnCreeps = true;
        StartCoroutine(SpawnCreepsCoroutine());
    }

    public IEnumerator SpawnCreepsCoroutine()
    {
        while (willSpawnCreeps)
        {
            //Spawn Melee
            for (int i = 0; i < creepSpawnerData.meleePerWave; i ++)
            {
                if (willSpawnSuperMelee)
                {
                    Spawn(creepSpawnerData.superMeleeCreep);
                } else
                {
                    Spawn(creepSpawnerData.meleeCreep);
                }
                yield return new WaitForSeconds(creepSpawnerData.spawnInterval);
            }

            //Spawn Ranged
            for (int i = 0; i < creepSpawnerData.rangePerWave; i++)
            {
                if (willSpawnSuperRanged)
                {
                    Spawn(creepSpawnerData.superRangeCreep);
                } else
                {
                    Spawn(creepSpawnerData.rangeCreep);
                }
                yield return new WaitForSeconds(creepSpawnerData.spawnInterval);
            }

            //Spawn Siege
            if (currentWave % creepSpawnerData.siegeWaveInterval == 0)
            {
                for (int i = 0; i < creepSpawnerData.siegePerWave; i++)
                {
                    if (willSpawnSuperSiege)
                    {
                        Spawn(creepSpawnerData.superSiegeCreep);
                    } else
                    {
                        Spawn(creepSpawnerData.siegeCreep);
                    }
                    yield return new WaitForSeconds(creepSpawnerData.spawnInterval);
                }
            }

            willSpawnCreeps = false;

            yield return null;
        }
    }

    private void Spawn(Creep creepToSpawn)
    {
        if (!spawnPoint) return;

        Creep creep;
        creep = Instantiate(creepToSpawn, spawnPoint.position, Quaternion.identity);

        (creep.controller as CreepController).laneWaypoints = laneWaypoints;
    }

    private void SpawnSuperMelee(Unit victim, Unit instigator)
    {
        willSpawnSuperMelee = true;
        
        if (willSpawnSuperRanged)
        {
            willSpawnSuperSiege = true;
        }

        AnnounceBarracksDestruction(victim as Barracks);
    }

    private void SpawnSuperRanged(Unit victim, Unit instigator)
    {
        willSpawnSuperRanged = true;

        if (willSpawnSuperMelee)
        {
            willSpawnSuperSiege = true;
        }

        AnnounceBarracksDestruction(victim as Barracks);
    }

    public void ScaleCreeps()
    {
        for (int i = 0; i < creeps.Count; i++)
        {
            creeps[i].data.ScaleCreep();
        }
    }

    // Automatically reset creep data sana at the start of every game
    // because when the creeps scale, their datas permanently change and need to be changed in the editor pa
    private void ResetCreepDatas()
    {
        //creepSpawnerData.meleeCreep.stats = creepSpawnerData.baseMeleeCreep;
        //creepSpawnerData.superMeleeCreep.stats = creepSpawnerData.baseSuperMeleeCreep;
        //creepSpawnerData.rangeCreep.stats = creepSpawnerData.baseRangedCreep;
        //creepSpawnerData.superRangeCreep.stats = creepSpawnerData.baseSuperRangedCreep;
    }

    private void PopulateCreepList()
    {
        creeps.Add(creepSpawnerData.meleeCreep);
        creeps.Add(creepSpawnerData.superMeleeCreep);

        creeps.Add(creepSpawnerData.rangeCreep);
        creeps.Add(creepSpawnerData.superRangeCreep);

        creeps.Add(creepSpawnerData.siegeCreep);
        creeps.Add(creepSpawnerData.superSiegeCreep);
    }

    #region Debug Logs

    private void AnnounceBarracksDestruction(Barracks destroyedBarracks)
    {
        string barracksTypeName = "Unknown";
        switch (destroyedBarracks.barracksType)
        {
            case BarracksType.Melee:
                barracksTypeName = "Melee";
                break;
            case BarracksType.Ranged:
                barracksTypeName = "Ranged";
                break;
            default:
                Debug.LogError("Barrack's Type invalid");
                break;
        }

        switch (destroyedBarracks.faction)
        {
            case Faction.Radiant:
                AnnounceLane("Radiant", "Dire", barracksTypeName);
                break;
            case Faction.Dire:
                AnnounceLane("Dire", "Radiant", barracksTypeName);
                break;
            default:
                Debug.LogError("Barrack's Faction invalid");
                break;
        }
    }

    private void AnnounceLane(string lostFactionName, string winFactionName, string barracksType)
    {
        switch (lane)
        {
            case Lane.Top:
                Debug.Log(lostFactionName + " top " + barracksType + " barracks destroyed.");
                break;
            case Lane.Middle:
                Debug.Log(lostFactionName + " middle " + barracksType + " barracks destroyed.");
                break;
            case Lane.Bottom:
                Debug.Log(lostFactionName + " bottom " + barracksType + " barracks destroyed.");
                break;
            default:
                Debug.LogError("Barrack's Lane invalid");
                break;
        }
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        if (laneWaypoints[0] == null) return;

        Gizmos.color = Color.cyan;
        Vector3 yOffset = Vector3.up * 5.0f;

        Gizmos.DrawLine(
            transform.position + yOffset,
            laneWaypoints[0].transform.position + yOffset
            );
         
        for (int i = 0; i < laneWaypoints.Count - 1; i++)
        {          
            Gizmos.DrawLine(
                laneWaypoints[i].transform.position + yOffset,
                laneWaypoints[i + 1].transform.position + yOffset
                );
        }
    }
}
