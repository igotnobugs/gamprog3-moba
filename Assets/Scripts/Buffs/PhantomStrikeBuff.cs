using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Buff / Unique / Phantom Strike Buff")]
public class PhantomStrikeBuff : Buff
{
    [BoxGroup("Buff Levels")]
    public Leveled<int> attackSpeedBonus = new Leveled<int>(100);

    public override void OnActivate()
    {
        unitAffected.data.attackSpeed.flatModifier += attackSpeedBonus.At(buffLevel);

        unitAffected.AddBuff(this);
    }

    public override void OnDeactivate()
    {
        unitAffected.data.attackSpeed.flatModifier -= attackSpeedBonus.At(buffLevel);
        unitAffected.RemoveBuff(this);
    }
}
