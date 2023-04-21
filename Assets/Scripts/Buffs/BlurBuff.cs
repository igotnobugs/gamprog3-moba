using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Buff / Unique / Blur Buff")]
public class BlurBuff : Buff
{
    [BoxGroup("Buff Levels")]
    public Leveled<int> attackSpeedPerStack;


    private int attackSpeedBonus = 0;

    public override void OnActivate()
    {
        attackSpeedBonus = attackSpeedPerStack.At(buffLevel) * stacks;
        unitAffected.data.attackSpeed.flatModifier += attackSpeedBonus;
        unitAffected.AddBuff(this);
    }

    public override void OnDeactivate()
    {
        unitAffected.data.attackSpeed.flatModifier -= attackSpeedBonus;
        unitAffected.RemoveBuff(this);
    }

    protected override void UpdateStack()
    {
        base.UpdateStack();

        unitAffected.data.attackSpeed.flatModifier -= attackSpeedBonus;
        attackSpeedBonus = attackSpeedPerStack.At(buffLevel) * stacks;
        unitAffected.data.attackSpeed.flatModifier += attackSpeedBonus;
    }
}
