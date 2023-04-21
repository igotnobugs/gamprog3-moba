using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill / Unique / Blur Skill")]
public class BlurSkill : Skill
{
    public Leveled<int> permanentEvasionBuff = new Leveled<int>(20);

    public override void OnActivate(Unit target = null)
    {
        unitOwner.data.evasion.normal += permanentEvasionBuff.At(skillLevel);
        unitOwner.stateEvents.OnEvade.AddListener(ApplyBlur);
    }

    public override void OnDeactivate()
    {
        unitOwner.data.evasion.normal -= permanentEvasionBuff.At(skillLevel);
        unitOwner.stateEvents.OnEvade.RemoveListener(ApplyBlur);
    }

    public override void OnUpgradeSkill()
    {
        unitOwner.data.evasion.normal -= permanentEvasionBuff.At(skillLevel - 1);
        unitOwner.data.evasion.normal += permanentEvasionBuff.At(skillLevel);
    }


    private void ApplyBlur()
    {
        ApplyBuff(unitOwner);
    }

    public override void OnActivate(Vector3 targetPosition)
    {
        throw new System.NotImplementedException();
    }
}
