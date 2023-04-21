using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Buff / Unique / Guardian Sprint Buff")]
public class GuardianSprintBuff : Buff
{
    [BoxGroup("Buff Levels")]
    public Leveled<float> moveSpeedbonus = new Leveled<float>(0.2f);

    [BoxGroup("Buff Levels")]
    public Leveled<int> attackSpeedBonus = new Leveled<int>(20);


    public override void OnActivate()
    {
        unitAffected.data.moveSpeed.percentModifier += moveSpeedbonus.At(buffLevel);
        unitAffected.data.attackSpeed.normal += attackSpeedBonus.At(buffLevel);

        unitAffected.AddBuff(this);
    }

    public override void OnDeactivate()
    {
        unitAffected.data.moveSpeed.percentModifier -= moveSpeedbonus.At(buffLevel);
        unitAffected.data.attackSpeed.normal -= attackSpeedBonus.At(buffLevel);

        unitAffected.RemoveBuff(this);
    }
}
