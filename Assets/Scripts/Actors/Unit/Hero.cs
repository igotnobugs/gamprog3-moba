using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Unit
{
    public HeroLevelDatabase levelDatabase;

    [SerializeField]
    public GameObject model;

    private CapsuleCollider heroCollider;

    [Header("Hide When Dead")]
    public GameObject minimapIcon;
    public GameObject unitCircle;
    public GameObject healthbarHUD;

    protected override void Awake()
    {
        base.Awake();

        heroCollider = GetComponent<CapsuleCollider>();
    }

    private void Start()
    {
        //spawnedHeroes.Add(this);
        GameManager.AddHero(this);
    }

    public void Respawn()
    {
        data.health.current = data.health.max;
        data.mana.current = data.mana.max;

        model.SetActive(true);
        minimapIcon.SetActive(true);
        unitCircle.SetActive(true);
        healthbarHUD.SetActive(true);

        if (faction == Faction.Radiant)
        {
            Warp(GameManager.Instance.radiantHeroSpawnpoint.groundPosition);
        } else
        {
            Warp(GameManager.Instance.direHeroSpawnpoint.groundPosition);
        }
        heroCollider.enabled = true;
        isAlive = true;

        stateEvents.OnUnitRespawn?.Invoke();
    }

    protected override void Update()
    {
        base.Update();

        if (isAlive)
        {
            data.health.Regenerate();
            data.mana.Regenerate();
        } else
        {
            data.respawnTime -= Time.deltaTime;
            if (data.respawnTime <= 0)
            {
                Respawn();
            }
        }
    }

    protected override void OnUnitDeath()
    {
        OnUnSelected();

        minimapIcon.SetActive(false);
        unitCircle.SetActive(false);
        healthbarHUD.SetActive(false);
        data.respawnTime = data.levelData.respawnTime;

        model.SetActive(false);
        heroCollider.enabled = false;
    }
}