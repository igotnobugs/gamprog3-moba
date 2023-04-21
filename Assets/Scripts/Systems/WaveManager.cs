using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    private bool willSpawnWaves = true;

    [SerializeField]
    private float waveInterval = 30.0f; 

    [SerializeField] 
    private List<CreepSpawner> creepSpawners = new List<CreepSpawner>();

    private int waveNumber = 0;

    [SerializeField]
    private float scaleCreepInterval = 180.0f;

    private void OnEnable()
    {
        Ancient.OnVictory.AddListener(StopSpawning);
    }

    private void OnDisable()
    {
        Ancient.OnVictory.RemoveListener(StopSpawning);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWaveCoroutine());
        StartCoroutine(PowerupCoroutine());
    }

    private IEnumerator SpawnWaveCoroutine()
    {
        while (willSpawnWaves)
        {
            waveNumber++;

            for (int i = 0; i < creepSpawners.Count; i++)
            {
                creepSpawners[i].SpawnCreeps(waveNumber);
            }
            yield return new WaitForSeconds(waveInterval);

            yield return null;
        }
    }

    private IEnumerator PowerupCoroutine()
    {
        while(willSpawnWaves)
        {
            yield return new WaitForSeconds(scaleCreepInterval);

            creepSpawners[0].ScaleCreeps();     // only call this once using ANY spawner because the creep data in both Dire and Range spawners are the same
            yield return null;
        }
    }

    private void StopSpawning()
    {
        willSpawnWaves = false;
    }
}
