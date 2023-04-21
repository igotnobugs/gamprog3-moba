using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill / Melee")]

public class MeleeAttack : Skill
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
