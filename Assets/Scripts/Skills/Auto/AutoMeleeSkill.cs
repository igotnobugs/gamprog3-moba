using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Auto skills uses attackDamage of unit

[CreateAssetMenu(menuName = "Skill / Auto / Melee")]
public class AutoMeleeSkill : Skill
{
    public override void OnActivate(Unit target = null)
    {
        if (!unitOwner.currentTarget) return;

        unitOwner.currentTarget.TakeDamage(unitOwner.data.attackDamage.GetTotal(), damageType, unitOwner);
    }

    public override void OnActivate(Vector3 targetPosition)
    {
        throw new System.NotImplementedException();
    }

    public override void OnDeactivate()
    {
        throw new System.NotImplementedException();
    }
}
