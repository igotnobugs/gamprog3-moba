using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UnitWorldUI : MonoBehaviour
{
    [SerializeField]
    private bool showHealthBar = true;

    [SerializeField]
    private GameObject healthBar;

    [SerializeField]
    private Image healthBarFrames;

    [SerializeField]
    private Image healthBarForeground;

    [SerializeField]
    private HPParticle hpParticle;

    [SerializeField]
    private bool showManaBar = false;

    [SerializeField]
    private GameObject manaBar;

    [SerializeField]
    private Image manaBarFrames;

    [SerializeField]
    private Image manaBarForeground;

    [SerializeField]
    private GameObject stunDurationPanel;

    [SerializeField]
    private Image stunDurationBar;

    private float pointsPerBar = 500.0f;
    private float pixelsMultiplier = 255.0f;
    private Unit trackedObject;
    private float stunMaxDuration;

    private void Awake()
    {
        if (GetComponentInParent<Unit>() == null)
        { 
            enabled = false;
            return;
        }

        trackedObject = GetComponentInParent<Unit>();
    }

    private void OnEnable()
    {
        trackedObject.stateEvents.OnTakeDamage.AddListener(UpdateHealth);
        trackedObject.stateEvents.OnConsumeMana.AddListener(UpdateMana);
        trackedObject.stateEvents.OnStunned.AddListener(UpdateStun);
        trackedObject.stateEvents.OnEvade.AddListener(PopMiss);
    }

    private void OnDisable()
    {
        trackedObject.stateEvents.OnTakeDamage.RemoveListener(UpdateHealth);
        trackedObject.stateEvents.OnConsumeMana.RemoveListener(UpdateMana);
        trackedObject.stateEvents.OnStunned.RemoveListener(UpdateStun);
        trackedObject.stateEvents.OnEvade.RemoveListener(PopMiss);
    }

    // Start is called before the first frame update
    private void Start()
    {
        float hpRatio = trackedObject.data.health.max / pointsPerBar;
        healthBarFrames.pixelsPerUnitMultiplier = pixelsMultiplier * hpRatio;

        healthBar.SetActive(showHealthBar);

        if (!showManaBar) return;
        float manaRatio = trackedObject.data.mana.max / pointsPerBar;
        manaBarFrames.pixelsPerUnitMultiplier = pixelsMultiplier * manaRatio;

        manaBar.SetActive(showManaBar);
    }

    private void Update()
    {
        if (trackedObject.data.stunState > 0)
        {
            stunDurationPanel.SetActive(true);
            stunDurationBar.fillAmount = trackedObject.data.stunState / stunMaxDuration;
        } else
        {
            stunDurationPanel.SetActive(false);
        }

        if (healthBar.activeSelf)
        {
            healthBarForeground.fillAmount = trackedObject.data.health.GetPercent();
        }

        //if (manaBar.activeSelf)
        //{
        //    manaBarForeground.fillAmount = trackedObject.stats.mana.GetPercent();
        //}
    }

    private void UpdateHealth(float damageAmount)
    {
        PopDamage(damageAmount);
    }

    private void PopMiss()
    {
        PopText("Miss");
    }
    private void UpdateMana(float amount)
    {
        //if(manaBar.activeSelf)
        //{
        //    manaBarForeground.fillAmount = trackedObject.stats.mana.GetPercent();
        //}
    }

    private void UpdateStun(float duration)
    {
        stunMaxDuration = duration;
    }

    private void PopDamage(float amount)
    {
        HPParticle spawnedHP = Instantiate(hpParticle, transform.position, Quaternion.identity);
        spawnedHP.SetDamageAmount(amount);
    }

    private void PopText(string text)
    {
        HPParticle spawnedHP = Instantiate(hpParticle, transform.position, Quaternion.identity);
        spawnedHP.SetText(text);

    }
}
