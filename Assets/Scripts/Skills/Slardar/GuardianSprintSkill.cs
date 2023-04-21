using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill / Unique / Guardian Sprint")]
public class GuardianSprintSkill : Skill
{
    public override void OnActivate(Unit target = null)
    {
        ApplyBuff(unitOwner);
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
