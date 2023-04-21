using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill / Unique / Phantom Strike")]
public class PhantomStrikeSkill : Skill
{
    //public float distanceAwayWarp = 150.0f;

    public override void OnActivate(Unit target = null)
    {
        //ApplyBuff(target);
        unitOwner.Warp(target.transform.position 
            + (unitOwner.transform.forward * unitOwner.width.ToUnitValue()));

        if (target.faction != unitOwner.faction)
        {
            ApplyBuff(unitOwner);
            unitOwner.Attack(target);
        }
    }

    public override void OnDeactivate()
    {
        throw new System.NotImplementedException();
    }

    public override void OnActivate(Vector3 targetPosition)
    {
        throw new System.NotImplementedException();
    }
}
