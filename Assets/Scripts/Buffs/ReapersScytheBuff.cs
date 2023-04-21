using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Buff / Unique / Reapers Scythe Buff")]
public class ReapersScytheBuff : Buff
{
    public float stunDuration;

    [BoxGroup("Buff Levels")]
    public Leveled<float> damagePerMissingHealth = new Leveled<float>(0.7f);



    public override void OnActivate()
    {

        unitAffected.Stun(stunDuration);
        

        unitAffected.AddBuff(this);
    }

    public override void OnDeactivate()
    {
        float missingHealth = unitAffected.data.health.max - unitAffected.data.health.current;
        float damage = missingHealth * damagePerMissingHealth.At(buffLevel);
        unitAffected.TakeDamage(damage, DamageType.Magical, unitOwner);

        unitAffected.RemoveBuff(this);
    }
}
