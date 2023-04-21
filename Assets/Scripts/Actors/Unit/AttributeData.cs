using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[System.Serializable]
public class AttributeData
{
    public MainAttribute mainAttribute;

    [HorizontalLine(color: EColor.Red)]

    public Stat strength = new Stat(0, true, 0, 9999);

    public Stat agility = new Stat(0, true, 0, 9999);

    public Stat intelligence = new Stat(0, true, 0, 9999);

    [HideInInspector]
    public float damageBonus = 0;

    private float healthBonus = 0;
    private float healthRegenBonus = 0;
    private float armorBonus = 0;
    private float attackSpeedBonus = 0;
    private float manaBonus = 0;
    private float manaRegenBonus = 0;

    public void DeriveStats(UnitData stats)
    {
        ModifyStrength(stats, 0);
        ModifyAgility(stats, 0);
        ModifyIntelligence(stats, 0);
    }

    public void ModifyStrength(UnitData stats, float addAmount, bool isModifier = false)
    {
        stats.health.Modify(-healthBonus);
        stats.health.ModifyRegen(-healthRegenBonus);

        if (!isModifier) strength.normal += addAmount;
        else strength.flatModifier += addAmount;

        healthBonus = strength.GetTotal() * 20.0f;
        healthRegenBonus = strength.GetTotal() * 0.1f;

        stats.health.Modify(healthBonus);
        stats.health.ModifyRegen(healthRegenBonus);

        UpdateDamageBonus(stats);
    }

    public void ModifyAgility(UnitData stats, float addAmount, bool isModifier = false)
    {
        stats.armor.normal -= armorBonus;
        stats.attackSpeed.normal -= attackSpeedBonus;

        if (!isModifier) agility.normal += addAmount;
        else agility.flatModifier += addAmount;

        armorBonus = strength.GetTotal() * 0.16f;
        attackSpeedBonus = strength.GetTotal() * 1;

        stats.armor.normal += armorBonus;
        stats.attackSpeed.normal += attackSpeedBonus;

        //stats.UpdateTotalAttackSpeed();
        UpdateDamageBonus(stats);
    }

    public void ModifyIntelligence(UnitData stats, float addAmount, bool isModifier = false)
    {
        stats.mana.Modify(-manaBonus);
        stats.mana.ModifyRegen(-manaRegenBonus);

        if (!isModifier) intelligence.normal += addAmount;
        else intelligence.flatModifier += addAmount;

        manaBonus = intelligence.GetTotal() * 12.0f;
        manaRegenBonus = intelligence.GetTotal() * 0.05f;

        stats.mana.Modify(manaBonus);
        stats.mana.ModifyRegen(manaRegenBonus);

        UpdateDamageBonus(stats);
    }

    public void UpdateDamageBonus(UnitData stats)
    {
        stats.attackDamage.normal -= damageBonus;
        switch (mainAttribute)
        {
            case MainAttribute.Strength:
                damageBonus = strength.GetTotal();
                break;
            case MainAttribute.Agility:
                damageBonus = agility.GetTotal();
                break;
            case MainAttribute.Intelligence:
                damageBonus = intelligence.GetTotal();
                break;
        }
        stats.attackDamage.normal += damageBonus;
    }

    public void GrowAttributes()
    {
        strength.Grow();
        agility.Grow();
        intelligence.Grow();
    }

    public void IncreaseAllAttributes(int amount, bool asModifier = false)
    {
        strength.normal += amount;
        agility.normal += amount;
        intelligence.normal += amount;

    }
}