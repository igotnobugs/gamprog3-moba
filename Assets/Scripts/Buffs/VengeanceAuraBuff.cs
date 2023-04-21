using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Buff / Unique / Vengeance Aura Buff")]
public class VengeanceAuraBuff : Buff
{
    [BoxGroup("Buff Levels")]
    public Leveled<int> attackRangeModifier = new Leveled<int>(50);

    [BoxGroup("Buff Levels")]
    public Leveled<int> attackDamageModifier = new Leveled<int>(5);

    public override void OnActivate()
    {
        unitAffected.data.attackRange.flatModifier += attackDamageModifier.At(buffLevel);
        unitAffected.data.attackDamage.flatModifier += attackDamageModifier.At(buffLevel);

        unitAffected.AddBuff(this);
    }

    public override void OnDeactivate()
    {
        unitAffected.data.attackRange.flatModifier -= attackDamageModifier.At(buffLevel);
        unitAffected.data.attackDamage.flatModifier -= attackDamageModifier.At(buffLevel);
        unitAffected.RemoveBuff(this);
    }
}
