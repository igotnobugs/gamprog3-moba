using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UnitStatsDisplayUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    #region UI
    [SerializeField]
    private GameObject detailedStatsPanel;
    
    [Header("Attack")]
    [SerializeField]
    private TextMeshProUGUI unitAttackSpeedText;
    [SerializeField]
    private TextMeshProUGUI unitDamageText;
    [SerializeField]
    private TextMeshProUGUI unitAttackRangeText;
    [SerializeField]
    private TextMeshProUGUI unitMoveSpeedText;
    [SerializeField]
    private TextMeshProUGUI unitManaRegenText;

    [Header("Defense")]
    [SerializeField]
    private TextMeshProUGUI unitArmorText;
    [SerializeField]
    private TextMeshProUGUI unitPhysicalResistText;
    [SerializeField]
    private TextMeshProUGUI unitMagicResText;
    [SerializeField]
    private TextMeshProUGUI unitEvasionText;
    [SerializeField]
    private TextMeshProUGUI unitHealthRegenText;

    [Header("Attributes")]
    [SerializeField]
    private TextMeshProUGUI unitStrText;
    [SerializeField]
    private TextMeshProUGUI unitStrBonusText;
    [SerializeField]
    private TextMeshProUGUI unitAgiText;
    [SerializeField]
    private TextMeshProUGUI unitAgiBonusText;
    [SerializeField]
    private TextMeshProUGUI unitIntText;
    [SerializeField]
    private TextMeshProUGUI unitIntBonusText;
    [SerializeField]
    private Color primaryColor;
    [SerializeField]
    private Image strengthPanel;
    [SerializeField]
    private Image agilityPanel;
    [SerializeField]
    private Image intelligencePanel;
    #endregion

    private bool isStrengthPrimary = false;
    private bool isAgilityPrimary = false;
    private bool isIntelligencePrimary = false;

    private Unit trackedUnit;
    private UnitData trackedData;

    private void OnEnable()
    {
        GameManager.gameEvents.OnSelectedUnit.AddListener(SetTrackedUnit);
    }

    private void OnDisable()
    {
        if (GameManager.Instance)
            GameManager.gameEvents.OnSelectedUnit.RemoveListener(SetTrackedUnit);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        detailedStatsPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        detailedStatsPanel.SetActive(false);
    }

    private void SetTrackedUnit(Unit unit)
    {
        trackedUnit = unit;
        trackedData = unit.data;
    }

    private void LateUpdate()
    {
        if (trackedData)
        {
            //Attack
            SetAttackSpeedText(trackedData.attackSpeed.GetTotal(), trackedData.totalAttackTime);
            SetDamageText(trackedData.attackDamage.normal, trackedData.attackDamage.flatModifier);
            SetAttackRangeText(trackedData.attackRange.normal, trackedData.attackRange.flatModifier);
            SetMoveSpeedText(trackedData.moveSpeed.normal, trackedData.moveSpeed.GetTotal() - trackedData.moveSpeed.normal);
            SetManaRegenText(trackedData.mana.regen, trackedData.mana.regenModifier);

            SetArmorText(trackedData.armor.normal, trackedData.armor.flatModifier);

            //Defences
            SetText(unitPhysicalResistText, "Physical Resist",
                trackedData.physicalResistance.normal, trackedData.physicalResistance.percentModifier, "%");
            SetMagicResText(trackedData.GetTotalMagicResPercentage());
            SetText(unitEvasionText, "Evasion", 
                trackedData.evasion.normal, trackedData.evasion.flatModifier, "%");
            SetHealthRegenText(trackedData.health.regen, trackedData.health.regenModifier);

            //Attributes
            ConfigureAttributesPanel(trackedData.attributeData.mainAttribute);
            SetStrText(trackedData.attributeData.strength.normal, trackedData.attributeData.strength.growth);
            SetStrBonusText(trackedData.health.max, trackedData.health.regen, trackedData.GetTotalMagicResPercentage(), trackedData.attributeData.damageBonus);
            SetAgiText(trackedData.attributeData.agility.normal, trackedData.attributeData.agility.growth);
            SetAgiBonusText(trackedData.armor.normal, trackedData.attackSpeed.GetTotal(), trackedData.moveSpeed.GetTotal(), trackedData.attributeData.damageBonus);
            SetIntText(trackedData.attributeData.intelligence.normal, trackedData.attributeData.intelligence.growth);
            SetIntBonusText(trackedData.mana.max, trackedData.mana.regen, trackedData.attributeData.damageBonus);
        }
    }

    #region Setting Text
    public void SetText(TextMeshProUGUI textMesh, string label, float value, float valueMod, string unit = "")
    {
        if (valueMod > 0)
            textMesh.text = $"{label}:   {RoundToTwoDeciPlaces(value)} <color=green>+ {RoundToTwoDeciPlaces(valueMod)}{unit}</color>";
        else if (valueMod < 0)
            textMesh.text = $"{label}:   {RoundToTwoDeciPlaces(value)} <color=red> {RoundToTwoDeciPlaces(valueMod)}{unit}</color>";
        else
            textMesh.text = $"{label}:   {RoundToTwoDeciPlaces(value)}{unit}";
    }

    public void SetAttackSpeedText(float speed, float attackTime)
    {
        unitAttackSpeedText.text = $"Attack Speed:   {speed} ({RoundToTwoDeciPlaces(attackTime)}s)";
    }



    public void SetDamageText(float damage, float damageMod)
    {
        SetText(unitDamageText, "Damage", damage, damageMod);

        /*if (damageMod > 0)
            unitDamageText.text = $"Damage:   {RoundToTwoDeciPlaces(damage)} <color=green>+ {RoundToTwoDeciPlaces(damageMod)}</color>";
        else if(damageMod < 0)
            unitDamageText.text = $"Damage:   {RoundToTwoDeciPlaces(damage)} <color=red> {RoundToTwoDeciPlaces(damageMod)}</color>";
        else
            unitDamageText.text = $"Damage:   {RoundToTwoDeciPlaces(damage)}";*/
    }

    public void SetAttackRangeText(float attackRange, float attackRangeMod)
    {
        SetText(unitAttackRangeText, "Attack Range", attackRange, attackRangeMod);
        /*
        if (attackRangeMod > 0)
            unitAttackRangeText.text = $"Attack Range:   {Mathf.FloorToInt(attackRange)} <color=green>+ {Mathf.FloorToInt(attackRangeMod)}</color>";
        else if(attackRangeMod < 0)
            unitAttackRangeText.text = $"Attack Range:   {Mathf.FloorToInt(attackRange)} <color=red> {Mathf.FloorToInt(attackRangeMod)}</color>";
        else
            unitAttackRangeText.text = $"Attack Range:   {Mathf.FloorToInt(attackRange)}";
        */
    }

    public void SetMoveSpeedText(float moveSpeed, float moveSpeedMod)
    {
        if (moveSpeedMod > 0)
            unitMoveSpeedText.text = $"Movespeed:   {Mathf.FloorToInt(moveSpeed)} <color=green>+ {RoundToTwoDeciPlaces(moveSpeedMod)}</color>";
        else if(moveSpeedMod < 0)
            unitMoveSpeedText.text = $"Movespeed:   {Mathf.FloorToInt(moveSpeed)} <color=red> {RoundToTwoDeciPlaces(moveSpeedMod)}</color>";
        else
            unitMoveSpeedText.text = $"Movespeed:   {Mathf.FloorToInt(moveSpeed)}";
    }

    public void SetManaRegenText(float manaRegen, float manaRegenMod)
    {
        if (manaRegenMod > 0)
            unitManaRegenText.text = $"Mana Regen:   {RoundToTwoDeciPlaces(manaRegen)} <color=green>+ {RoundToTwoDeciPlaces(manaRegenMod)}</color>";
        else if(manaRegenMod < 0)
            unitManaRegenText.text = $"Mana Regen:   {RoundToTwoDeciPlaces(manaRegen)} <color=red> {RoundToTwoDeciPlaces(manaRegenMod)}</color>";
        else
            unitManaRegenText.text = $"Mana Regen:   {RoundToTwoDeciPlaces(manaRegen)}";
    }

    public void SetArmorText(float armor, float armorMod)
    {
        if (armorMod > 0)
            unitArmorText.text = $"Armor:   {RoundToTwoDeciPlaces(armor)} <color=green>+ {RoundToTwoDeciPlaces(armorMod)}</color>";
        else if(armorMod < 0)
            unitArmorText.text = $"Armor:   {RoundToTwoDeciPlaces(armor)} <color=red> {RoundToTwoDeciPlaces(armorMod)}</color>";
        else
            unitArmorText.text = $"Armor:   {RoundToTwoDeciPlaces(armor)}";
    }

    public void SetMagicResText(float magicRes)
    {
        unitMagicResText.text = $"Magic Resist:   {RoundToTwoDeciPlaces(magicRes)}%";
    }

    public void SetHealthRegenText(float healthRegen, float healthRegenMod)
    {
        if (healthRegenMod > 0)
            unitHealthRegenText.text = $"Health Regen:   {RoundToTwoDeciPlaces(healthRegen)} <color=green>+ {RoundToTwoDeciPlaces(healthRegenMod)}</color>";
        else if(healthRegenMod < 0)
            unitHealthRegenText.text = $"Health Regen:   {RoundToTwoDeciPlaces(healthRegen)} <color=red> {RoundToTwoDeciPlaces(healthRegenMod)}</color>";
        else
            unitHealthRegenText.text = $"Health Regen:   {RoundToTwoDeciPlaces(healthRegen)}";
    }

    public void SetStrText(float str, float strGrowth)
    {
        unitStrText.text = $"{str} (Gains {strGrowth} per lvl)";
    }

    public void SetStrBonusText(float maxHealth, float healthRegen, float magicRes, float damage)
    {
        if(isStrengthPrimary)
            unitStrBonusText.text = $"= {damage} Damage (Primary Role Bonus) \n= {maxHealth} HP, {healthRegen} HP Regen and {magicRes}% Magic Resistance";
        else
            unitStrBonusText.text = $"= {maxHealth} HP, {healthRegen} HP Regen and {magicRes}% Magic Resistance";
    }

    public void SetAgiText(float agi, float agiGrowth)
    {
        unitAgiText.text = $"{agi} (Gains {agiGrowth} per lvl)";
    }

    public void SetAgiBonusText(float totalArmor, float attackSpeed, float moveSpeed, float damage)
    {
        if (isAgilityPrimary)
            unitAgiBonusText.text = $"= {damage} Damage (Primary Role Bonus) \n= {totalArmor} Armor, {attackSpeed} Attack Speed and {moveSpeed} Movement Speed";
        else 
            unitAgiBonusText.text = $"= {totalArmor} Armor, {attackSpeed} Attack Speed and {moveSpeed} Movement Speed";
    }

    public void SetIntText(float intVal, float intGrowth)
    {
        unitIntText.text = $"{intVal} (Gains {intGrowth} per lvl)";
    }

    public void SetIntBonusText(float maxMana, float manaRegen, float damage)
    {
        if (isIntelligencePrimary)
            unitIntBonusText.text = $"= {damage} Damage (Primary Role Bonus) \n= {maxMana} Mana, {manaRegen} Mana Regen";
        else
            unitIntBonusText.text = $"= {maxMana} Mana, {manaRegen} Mana Regen";
    }

    public void ConfigureAttributesPanel(MainAttribute mainAttribute)
    {
        if (mainAttribute == MainAttribute.Strength)
        {
            strengthPanel.color = primaryColor;
            isStrengthPrimary = true;
        }
        else if (mainAttribute == MainAttribute.Agility)
        {
            agilityPanel.color = primaryColor;
            isAgilityPrimary = true;
        }
        else
        {
            intelligencePanel.color = primaryColor;
            isIntelligencePrimary = true;
        }
    }
    #endregion

    private float RoundToTwoDeciPlaces(float value)
    {
        return Mathf.Round(value * 100f) * 0.01f;
    }
}
