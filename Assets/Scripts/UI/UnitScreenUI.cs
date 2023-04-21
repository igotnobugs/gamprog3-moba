using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using TMPro;

public class UnitScreenUI : MonoBehaviour
{
    [SerializeField, Required]
    private Portrait portraitPanel;

    [SerializeField, Required]
    private Healthbar healthbar;

    [SerializeField, Required]
    private Manabar manabar;

    [SerializeField, Required]
    private LevelBar levelBar;

    private Unit trackedUnit;
    private UnitData trackedData;

    [SerializeField]
    private Button attributeLevelButton;

    private void OnEnable()
    {
        GameManager.gameEvents.OnSelectedUnit.AddListener(SetTrackedUnit);
    }

    private void OnDisable()
    {
        if (GameManager.Instance)
            GameManager.gameEvents.OnSelectedUnit.RemoveListener(SetTrackedUnit);
    }

    private void Update()
    {
        if (!trackedData && !trackedUnit) return;

        if (trackedUnit == GameManager.Instance.mainHero)
        {
            attributeLevelButton.gameObject.SetActive(trackedData.skillPoints > 0);
        } 
    }

    private void LateUpdate()
    {
        if (trackedData)
        {
            // Healthbar
            healthbar.SetHealthFill(trackedData.health.GetPercent());
            healthbar.SetHealthText(Mathf.Floor(trackedData.health.current), trackedData.health.max); // changed "trackedData.health" to Mathf.Floor(trackedData.health)
            //healthbar.ShowHealthRegenText(trackedData.health.current, trackedData.health.max, trackedData.health.regen);
            healthbar.SetHealthRegenText(trackedData.health.GetTotalRegen());

            // Manabar
            manabar.SetManaFill(trackedData.mana.GetPercent());
            manabar.SetManaText(Mathf.Floor(trackedData.mana.current), trackedData.mana.max);
            //manabar.ShowManaRegenText(trackedData.mana.current, trackedData.mana.max, trackedData.mana.regen);
            manabar.SetManaRegenText(trackedData.mana.GetTotalRegen());

            if(trackedData.unitType == UnitType.Hero)
            {
                // Levelbar
                Hero trackedHero = trackedUnit as Hero;
                levelBar.SetLevelFill(trackedData.GetExpPercentage());
                levelBar.SetLevelbarText(Mathf.Floor(trackedData.experience), trackedData.levelData.requiredExp);
                levelBar.SetLevelText(trackedData.level);
            }
            else
            {
                levelBar.SetLevelFill(0);
                levelBar.SetLevelbarText(0,0);
                levelBar.SetLevelText(0);
            }

            // Portrait panel
            portraitPanel.SetNameText(trackedData.unitName);

            portraitPanel.SetAttackText(trackedData.attackDamage.normal, trackedData.attackDamage.flatModifier);
            portraitPanel.SetArmorText(trackedData.armor.normal, trackedData.armor.flatModifier);
            portraitPanel.SetSpeedText(trackedData.moveSpeed.normal, trackedData.moveSpeed.flatModifier);
            portraitPanel.SetStrText(trackedData.attributeData.strength.normal);
            portraitPanel.SetAgiText(trackedData.attributeData.agility.normal);
            portraitPanel.SetIntText(trackedData.attributeData.intelligence.normal);
        }
    }

    public void SetTrackedUnit(Unit unit)
    {
        trackedUnit = unit;
        trackedData = unit.data;

        attributeLevelButton.gameObject.SetActive(unit == GameManager.Instance.mainHero);
    }

    //Level up basically
    public void IncreaseAttributePoints()
    {
        if (trackedData)
        {
            if (trackedData.skillPoints <= 0) return;
            trackedData.attributeData.IncreaseAllAttributes(2);
            trackedData.skillPoints--;
        }
    }
}
