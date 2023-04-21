using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Buff / Stat Modifier")]
public class StatModifierBuff : Buff
{
    //Unit targetUnit;

    public StatType statModified;

    [BoxGroup("Buff Levels")]
    public Leveled<float> modifierValue = new Leveled<float>(0);

    public override void OnActivate()
    {
        switch(statModified)
        {
            case StatType.Armor:
                unitAffected.data.armor.flatModifier += modifierValue.At(buffLevel);
                break;
            case StatType.AttackDamage:
                unitAffected.data.attackDamage.flatModifier += modifierValue.At(buffLevel);
                break;
            case StatType.AttackRange:
                unitAffected.data.attackRange.flatModifier += modifierValue.At(buffLevel);
                break;
            case StatType.MoveSpeed:
                unitAffected.data.moveSpeed.flatModifier += modifierValue.At(buffLevel);
                break;
            case StatType.StunState:
                unitAffected.Stun(modifierValue.At(buffLevel));
                break;
            default:
                Debug.Log("Stat doesnt exist.");
                break;
        }

        unitAffected.AddBuff(this);
    }

    public override void OnDeactivate()
    {
        switch (statModified)
        {
            case StatType.Armor:
                unitAffected.data.armor.flatModifier -= modifierValue.At(buffLevel);
                break;
            case StatType.AttackDamage:
                unitAffected.data.attackDamage.flatModifier -= modifierValue.At(buffLevel);
                break;
            case StatType.AttackRange:
                unitAffected.data.attackRange.flatModifier -= modifierValue.At(buffLevel);
                break;
            case StatType.MoveSpeed:
                unitAffected.data.moveSpeed.flatModifier -= modifierValue.At(buffLevel);
                break;
            case StatType.StunState:
                //unitTarget.stats.stunState = 0;
                break;
            default:
                Debug.Log("Stat doesnt exist.");
                break;
        }
        unitAffected.RemoveBuff(this);
    }
}